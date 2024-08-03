using Expenses.Data;
using expensesBE.Data.DTO.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace expensesBE.Data.DTO
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatisticsServices(AppDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<IdentityUser> GetCurrentUserAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IEnumerable<KeyValuePair<string, decimal>>> GetExpenseAmountPerCategory()
        {
            var user = await GetCurrentUserAsync();
            var expenses = _context.Expenses
                .Where(e => e.User.Id == user.Id)
                .GroupBy(e => e.Description)
                .Select(g => new KeyValuePair<string, decimal>(g.Key, g.Sum(x => x.Amount)))
                .ToList();

            return expenses;
        }
    }
}
