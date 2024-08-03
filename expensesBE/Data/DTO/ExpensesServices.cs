using Expenses.Data;
using expensesBE.Data;
using expensesBE.Data.DTO;
using expensesBE.Data.DTO.Interfaces;
using expensesBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace expensesBE.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly AppDbContext _context;

        public ExpensesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expenseDto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == expenseDto.UserId);
            if (!userExists)
            {
                return null;
            }

            var expense = new Expense
            {
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                UserId = expenseDto.UserId
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            expenseDto.Id = expense.Id;
            return expenseDto;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesAsync(ClaimsPrincipal user)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("unique_name")?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (dbUser == null)
            {
                return null;
            }

            var expenses = await _context.Expenses
                .Where(e => e.UserId == dbUser.Id)
                .Select(e => new ExpenseDTO
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    UserId = e.UserId
                })
                .ToListAsync();

            return expenses;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId, ClaimsPrincipal user)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("unique_name")?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (dbUser == null)
            {
                return false;
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.UserId == dbUser.Id);

            if (expense == null)
            {
                return false;
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDto, ClaimsPrincipal user)
        {
            var userName = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("unique_name")?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (dbUser == null)
            {
                return null;
            }

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseDto.Id && e.UserId == dbUser.Id);

            if (expense == null)
            {
                return null;
            }

            expense.Description = expenseDto.Description;
            expense.Amount = expenseDto.Amount;

            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();

            return expenseDto;
        }

    }
}
