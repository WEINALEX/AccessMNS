using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AccessMNS.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _currentUser = new ClaimsPrincipal(new ClaimsIdentity()); // Non authentifié

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var storedUser = await _sessionStorage.GetAsync<string>("authUser");

                if (storedUser.Success && !string.IsNullOrEmpty(storedUser.Value))
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
                    _currentUser = new ClaimsPrincipal(new ClaimsIdentity()); // Non authentifié
                }
            }
            catch
            {
                _currentUser = new ClaimsPrincipal(new ClaimsIdentity()); // En cas d'erreur, utilisateur non authentifié
            }

            return new AuthenticationState(_currentUser);
        }

        public async Task LoginAsync(string username)
        {
            await _sessionStorage.SetAsync("authUser", username);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            }, "session");

            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync("authUser");
            _currentUser = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }
    }
}