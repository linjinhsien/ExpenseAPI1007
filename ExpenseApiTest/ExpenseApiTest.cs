using ExpenseAPI.Controllers;
using ExpenseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseApiTest;
// 這個是用來測試 ExpenseAPI/Controllers/ExpenseController.cs 的測試程式
// 這個測試程式會使用 InMemoryDatabase 來測試
// 只要測試 POST 方法即可
// 使用AAA模式來撰寫測試程式
// 每一個方法至少提供三個測試案例,一個正向測試,一個反向測試,一個邊界測試
//測試名稱 要用 Given_When_Then 的方式命名
public class ExpenseControllerTest
{
    private readonly ExpenseContext _context;
    private readonly ExpenseController _controller;
    private readonly ILogger<ExpenseController> _logger;

    public ExpenseControllerTest()
    {
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "ExpenseList")
            .Options;

        _context = new ExpenseContext(options);
        _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ExpenseController>();
        _controller = new ExpenseController(_context, _logger);
    }

    [Fact]
    //一個正向測試,測試名稱 要用 Given_When_Then 的方式命名
    public async Task PostExpense_ValidExpense_ReturnsCreatedExpense()
    {
        // Arrange
        var newExpense = new Expense
        {
            Category = "行",
            Description = "捷運",
            Amount = 50,
            Date = DateTime.Parse("2021-01-01"),
            Title = "交通捷運費"
        };

        // Act
        var result = await _controller.PostExpense(newExpense);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdExpense = Assert.IsType<Expense>(createdAtActionResult.Value);
        Assert.Equal(newExpense.Category, createdExpense.Category);
        Assert.Equal(newExpense.Description, createdExpense.Description);
        Assert.Equal(newExpense.Amount, createdExpense.Amount);
        Assert.Equal(newExpense.Date, createdExpense.Date);
        Assert.Equal(newExpense.Title, createdExpense.Title);
    }
    [Fact]
    public async Task PostExpense_InvalidExpense_ReturnsBadRequest()
    {
        // Arrange
        var newExpense = new Expense
        {
            Category = "行",
            Description = "捷運",
            Amount = -50, // Invalid amount
            Date = DateTime.Parse("2021-01-01"),
            Title = "交通捷運費"
        };

        // Act
        var result = await _controller.PostExpense(newExpense);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    //一個邊界測試
    public async Task PostExpense_BoundaryExpense_ReturnsCreatedExpense()
    {
        // Arrange
        var newExpense = new Expense
        {
            Category = "行",
            Description = "捷運",
            Amount = 0, // Boundary amount
            Date = DateTime.Parse("2021-01-01"),
            Title = "交通捷運費"
        };

        // Act
        var result = await _controller.PostExpense(newExpense);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var createdExpense = Assert.IsType<Expense>(createdAtActionResult.Value);
        Assert.Equal(newExpense.Category, createdExpense.Category);
        Assert.Equal(newExpense.Description, createdExpense.Description);
        Assert.Equal(newExpense.Amount, createdExpense.Amount);
        Assert.Equal(newExpense.Date, createdExpense.Date);
        Assert.Equal(newExpense.Title, createdExpense.Title);
    }

    [Fact]
    public async Task PutExpense_PartialUpdateWithoutDate_KeepsOriginalDate()
    {
        // Arrange
        var existingExpense = new Expense
        {
            Category = "食",
            Description = "早餐",
            Amount = 80,
            Date = DateTime.Parse("2026-01-15"),
            Title = "早餐"
        };

        _context.Expenses.Add(existingExpense);
        await _context.SaveChangesAsync();

        var updateRequest = new Expense
        {
            Description = "午餐",
            Amount = 120
        };

        // Act
        var result = await _controller.PutExpense(existingExpense.Id, updateRequest);
        var updatedExpense = await _context.Expenses.FindAsync(existingExpense.Id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.NotNull(updatedExpense);
        Assert.Equal(DateTime.Parse("2026-01-15"), updatedExpense!.Date);
        Assert.Equal("午餐", updatedExpense.Description);
        Assert.Equal(120, updatedExpense.Amount);
    }

    [Fact]
    public async Task PutExpense_MismatchedId_ReturnsBadRequest()
    {
        // Arrange
        var updateRequest = new Expense
        {
            Id = 99,
            Description = "晚餐",
            Amount = 100,
            Date = DateTime.Parse("2026-01-10")
        };

        // Act
        var result = await _controller.PutExpense(1, updateRequest);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

}