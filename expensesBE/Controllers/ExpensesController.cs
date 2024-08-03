using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using expensesBE.Models;
using expensesBE.Data;
using System.Threading.Tasks;
using System.Linq;
using expensesBE.Data.DTO;
using Expenses.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using expensesBE.Services;


namespace expensesBE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ExpensesService _expensesService;


        public ExpensesController(AppDbContext context, ExpensesService expensesService)
        {
            _context = context;
            _expensesService = expensesService;

        }

        [HttpPost("AddExpense")]
        public async Task<IActionResult> AddExpense2([FromBody] ExpenseDTO expenseDto)
        {
            if (expenseDto == null)
            {
                return BadRequest("Expense data is null.");
            }

            var result = await _expensesService.AddExpenseAsync(expenseDto);

            if (result == null)
            {
                return BadRequest("UserId does not exist.");
            }

            return Ok(result);
        }

        [HttpGet("GetExpenses")]
        public async Task<IActionResult> GetExpenses2()
        {
            var expenses = await _expensesService.GetExpensesAsync(User);

            if (expenses == null)
            {
                return Unauthorized("User is not authenticated.");
            }

            if (!expenses.Any())
            {
                return NotFound("No expenses found for the user.");
            }

            return Ok(expenses);
        }

        [HttpDelete("DeleteExpense/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            var result = await _expensesService.DeleteExpenseAsync(expenseId, User);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("UpdateExpense")]
        public async Task<IActionResult> UpdateExpense([FromBody] ExpenseDTO expenseDto)
        {
            if (expenseDto == null)
            {
                return BadRequest("Expense data is null.");
            }

            var result = await _expensesService.UpdateExpenseAsync(expenseDto, User);

            if (result == null)
            {
                return NotFound("Expense not found or user not authorized.");
            }

            return Ok(result);
        }


    }
}
