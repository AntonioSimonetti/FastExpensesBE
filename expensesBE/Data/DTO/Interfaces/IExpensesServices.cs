using expensesBE.Models;
using System.Security.Claims;

namespace expensesBE.Data.DTO.Interfaces
{
    public interface IExpensesService
    {
        Task<ExpenseDTO> AddExpenseAsync(ExpenseDTO expenseDto);
        Task<IEnumerable<ExpenseDTO>> GetExpensesAsync(ClaimsPrincipal user);
        Task<ExpenseDTO> UpdateExpenseAsync(ExpenseDTO expenseDto, ClaimsPrincipal user);
        Task<bool> DeleteExpenseAsync(int expenseId, ClaimsPrincipal user);

    }
}

