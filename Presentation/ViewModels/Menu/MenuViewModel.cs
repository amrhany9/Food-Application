namespace FoodApplication.Presentation.ViewModel.Menu
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public List<ItemForMenuViewModel> Items { get; set; }
    }
}
