namespace expensesBE.Data.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
    }
}

