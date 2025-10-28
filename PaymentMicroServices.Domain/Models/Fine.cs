namespace PaymentMicroServices.Domain.Models
{
    public class Fine : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = default!;
        public DateTime DateIssued { get; set; }
        public bool IsPaid { get; set; }
    }
}
