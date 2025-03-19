using AccessMNS.Data;
using AccessMNS.Models;
using AccessMNS.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AccessMNS.Controllers
{
    public class UserController : DbContext
    {
        private readonly IDbRepository<User> _userDbRepository;
        private readonly ApplicationDbContext _dbContext;

        public UserController(IDbRepository<User> userDbRepository, ApplicationDbContext dbContext)
        {
            _userDbRepository = userDbRepository;
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userDbRepository.GetAllAsync();
        }

        public async Task<User?> GetByIdentity(string email, string password)
        {
            return await _dbContext.User.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }

        public async Task<List<User>> Filter(Func<User, bool> filter)
        {
            var users = await GetAllAsync();

            return users.Where(filter).ToList();
        }

        public async Task AddUser(User user)
        {
            await _userDbRepository.AddAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            await _userDbRepository.DeleteAsync(user);
        }

        public async Task DeleteUser(List<User> users)
        {
            foreach (var user in users)
            {
                await _userDbRepository.DeleteAsync(user);
            }
        }
    }
}
