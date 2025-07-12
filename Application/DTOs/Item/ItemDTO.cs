namespace FoodApplication.Application.DTOs.Item
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string ImageUrl { get; set; } = default!;

    }
}
