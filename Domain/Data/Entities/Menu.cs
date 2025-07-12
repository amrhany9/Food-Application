namespace FoodApplication.Domain.Data.Entities
{
    public class Menu : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }

        public List<Item> Items { get; set; }
    }
}
