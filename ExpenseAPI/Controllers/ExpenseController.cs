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

     

        // GET: /Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            _logger.LogInformation("Retrieving all expenses");
            return await _context.Expenses.ToListAsync();
        }

        // GET: /Expense/{id}
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

        // POST: /Expense
        //提供了如何使用 curl 呼叫的範例
        //curl -X POST -H "Content-Type: application/json" -d "{\"date\":\"2021-01-01\",\"description\":\"午餐\",\"amount\":500}" https://localhost:7039/Expense
        //並提到如果描述是午餐，且Amount範圍超過400,說明午餐不能夠報銷。
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {  
            if (expense.Description == "午餐" && expense.Amount > 400)
            {
                _logger.LogWarning("Lunch expense with amount over 400 is not allowed");
                return BadRequest("午餐費用不能超過400元");
            }
            if (expense.Amount < 0)
            {
                _logger.LogWarning("數量不能為負的");
                return BadRequest("數量不能為負的");
            }
            _context.Expenses.Add(expense);

            await _context.SaveChangesAsync();

            _logger.LogInformation("Created expense with id: {ExpenseId}", expense.Id);
            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }
    

        // PUT: /Expense/{id}
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

        // DELETE: /Expense/{id}
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