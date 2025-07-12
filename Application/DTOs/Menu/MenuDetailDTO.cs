namespace FoodApplication.Application.DTOs.Menu
{
    public class MenuDetailDTO : MenuDTO
    {
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<MenuItemDTO> Items { get; set; } = new();
    }
}
