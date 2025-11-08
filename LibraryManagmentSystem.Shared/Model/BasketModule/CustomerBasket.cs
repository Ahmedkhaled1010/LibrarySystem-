using LibraryManagmentSystem.Shared.DataTransferModel.BasketDto;

namespace LibraryManagmentSystem.Shared.Model.BasketModule
{
    public class CustomerBasket
    {

        public string UserId { get; set; }
        public ICollection<BasketItemDto> Items { get; set; } = [];

    }
}
