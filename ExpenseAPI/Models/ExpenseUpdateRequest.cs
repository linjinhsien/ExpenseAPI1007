namespace ExpenseAPI.Models
{
    public class ExpenseUpdateRequest
    {
        public int? Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public string? Category { get; set; }
        public string? Title { get; set; }
    }
}
