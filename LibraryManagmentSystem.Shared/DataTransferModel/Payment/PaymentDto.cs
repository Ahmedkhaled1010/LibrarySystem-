namespace LibraryManagmentSystem.Shared.DataTransferModel.Payment
{
    public class PaymentDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string BasketId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "EGP";

        public DateTime DateTime { get; set; }
    }
}
