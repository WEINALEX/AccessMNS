using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace AccessMNS.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        private const int SessionTimeoutMinutes = 30;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var storedUser = await _sessionStorage.GetAsync<string>("authUser");
                var storedExpiry = await _sessionStorage.GetAsync<string>("authExpiry");

                if (storedUser.Success && storedExpiry.Success &&
                    DateTime.TryParse(storedExpiry.Value, out var expiry) &&
                    DateTime.Now < expiry)
                {
                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, storedUser.Value),
                        new Claim(ClaimTypes.Role, "User")
                    }, "session");

                    _currentUser = new ClaimsPrincipal(identity);
                }
                else
                {
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
                    await LogoutAsync();
                }
            }
            catch
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            }

            return new AuthenticationState(_currentUser);
        }

        public async Task LoginAsync(string username)
        {
            var expiry = DateTime.Now.AddMinutes(SessionTimeoutMinutes);
            await _sessionStorage.SetAsync("authUser", username);
            await _sessionStorage.SetAsync("authExpiry", expiry.ToString());

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            }, "session");

            //_currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync("authUser");
            await _sessionStorage.DeleteAsync("authExpiry");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }
    }
}