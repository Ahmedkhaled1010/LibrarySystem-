using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Shared.Model.BasketModule
{
    public class BasketItem
    {
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Guid BookId { get; set; } = default!;
        public DateTime DateAdded { get; set; }
        public int Quantity { get; set; }

    }
}
