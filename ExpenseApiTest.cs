using Microsoft.Extensions.Logging;

public ExpenseControllerTest()
{
    var options = new DbContextOptionsBuilder<ExpenseContext>()
        .UseInMemoryDatabase(databaseName: "ExpenseList")
        .Options;
    _context = new ExpenseContext(options);
    var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ExpenseController>();
    _controller = new ExpenseController(_context, logger);
}
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
    Assert.IsType<BadRequestResult>(result.Result);
}

[Fact]
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
    }