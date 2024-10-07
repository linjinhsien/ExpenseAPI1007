// 這個用來代表一筆支出的Entity
// 標籤 Entity 通知/告訴系統要用一個 Table
// 通知 Entity 來源 Entity Framework Core 用來建立實體類別的 Table
// The entity contains four fields: Id, Date, Description, Amount,Category
// Id — This is the ID of the output (expense)
// Date — This is the date of the output (expense)
// Description — This is the description of the output (expense)
// Amount — This is the amount of the output (expense) float
// Category — This is the category of the output (expense


using System.ComponentModel.DataAnnotations;

namespace ExpenseAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}