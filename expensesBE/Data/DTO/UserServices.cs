using Microsoft.EntityFrameworkCore;
using Expenses.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using expensesBE.Data.DTO.Interfaces;

namespace expensesBE.Services
{
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _context;

        public UserServices(AppDbContext context)
        {
            _context = context;
        }

        public string GetUsername(ClaimsPrincipal user)
        {
            var username = user.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return null; 
            }

            return username;
        }

        public async Task<string?> GetUserIdAsync(ClaimsPrincipal user)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("unique_name")?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return null; 
            }

            var userEntity = await _context.Users
                                           .FirstOrDefaultAsync(u => u.UserName == userName);

            if (userEntity == null)
            {
                return null; 
            }

            return userEntity.Id; 
        }
    }
}
