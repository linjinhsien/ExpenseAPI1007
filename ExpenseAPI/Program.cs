using ExpenseAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DI注入
builder.Services.AddDbContext<ExpenseContext>(options =>
    options.UseInMemoryDatabase("ExpenseList"));
//datagenerator注入 ,
builder.Services.AddTransient<Datagenerator>();
// Build the service provider.
var serviceProvider = builder.Services.BuildServiceProvider();
// Obtain the service scope. ,同時我想try catch,且 log error
using (var scope = serviceProvider.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ExpenseContext>();
        var datagenerator = services.GetRequiredService<Datagenerator>();
        //初始化資料
        Datagenerator.Initialize(serviceProvider);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

