using FoodApplication.Application.DTOs.Menu;

namespace FoodApplication.Presentation.ViewModel.Menu
{
    public class MenuDetailViewModel
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<MenuItemDTO> Items { get; set; } = new();
    }
}
