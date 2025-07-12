namespace FoodApplication.Shared.ViewModel.Item
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
