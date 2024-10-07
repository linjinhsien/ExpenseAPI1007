using Microsoft.EntityFrameworkCore;

namespace ExpenseAPI.Models
{
    public class Datagenerator
    {     //使用ExpenseApi產生資料
          //分類為食衣住行四個
          //食: 早餐、午餐、晚餐、飲料
          //衣: 衣服、鞋子、配件
          //住: 房租、水電費、網路費
          //行: 捷運、公車、計程車
          //
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpenseContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExpenseContext>>()))
            {
                if (context.Expenses.Any())
                {
                    return;
                }

                context.Expenses.AddRange(
                    new Expense
                    {
                        Category = "食",
                        Description = "早餐",
                        Amount = 50,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "麥味登",
                    },
                    new Expense
                    {
                        Category = "食",
                        Description = "午餐",
                        Amount = 100,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "漢堡王",
                    },
                    new Expense
                    {
                        Category = "食",
                        Description = "晚餐",
                        Amount = 150,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "藏壽司",
                    },
                    new Expense
                    {
                        Category = "食",
                        Description = "飲料",
                        Amount = 50,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "德正",
                    },
                    new Expense
                    {
                        Category = "衣",
                        Description = "衣服",
                        Amount = 500,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "GUGGI",
                    },
                    new Expense
                    {
                        Category = "衣",
                        Description = "鞋子",
                        Amount = 300,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "愛迪達",
                    },
                    new Expense
                    {
                        Category = "衣",
                        Description = "配件",
                        Amount = 100,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "愛迪達"
                    },
                    new Expense
                    {
                        Category = "住",
                        Description = "房租",
                        Amount = 10000,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "付房租"
                    },
                    new Expense
                    {
                        Category = "住",
                        Description = "水電費",
                        Amount = 500,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "水電費"
                    },
                    new Expense
                    {
                        Category = "住",
                        Description = "網路費",
                        Amount = 500,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "網路費"
                       
                    },
                    new Expense
                    {
                        Category = "行",
                        Description = "捷運",
                        Amount = 50,
                        Date = DateTime.Parse("2021-01-01"),
                        Title = "交通捷運費"
                    },
                        new Expense
                        {
                            Category = "行",
                            Description = "公車",
                            Amount = 20,
                            Date = DateTime.Parse("2021-01-01")
                            ,
                            Title = "交通公車費"
                        });


                context.SaveChanges();
            }
        }
    }
}

