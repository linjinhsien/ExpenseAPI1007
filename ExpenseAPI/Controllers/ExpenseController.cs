//1 //這個 API Controller 用來處理 Expense 相關的 HTTP Reques
//這個 API Controller 會使用 ExpenseContext 來存取資料庫 _context = context;
// api path 是 /api/expense
// GET: api/Expense

using ExpenseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace ExpenseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;
        private readonly ILogger<ExpenseController> _logger;

        public ExpenseController(ExpenseContext context, ILogger<ExpenseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            _logger.LogInformation("Retrieving all expenses");
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expense/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            _logger.LogInformation("Retrieving expense with id: {ExpenseId}", id);
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                _logger.LogWarning("Expense with id: {ExpenseId} not found", id);
                return NotFound();
            }

            return expense;
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {
            _logger.LogInformation("Creating new expense");
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        // PUT: api/Expense/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                _logger.LogWarning("Mismatched expense id for update. Route id: {RouteId}, Expense id: {ExpenseId}", id, expense.Id);
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated expense with id: {ExpenseId}", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    _logger.LogWarning("Expense with id: {ExpenseId} not found during update", id);
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Expense/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                _logger.LogWarning("Expense with id: {ExpenseId} not found for deletion", id);
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted expense with id: {ExpenseId}", id);

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}