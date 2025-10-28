namespace PaymentMicroServices.Domain.Shared.Dtos
{
    public class FineDto
    {
        public string UserId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Reason { get; set; } = default!;
        public DateTime DateIssued { get; set; }
    }
}
