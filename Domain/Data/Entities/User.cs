namespace FoodApplication.Domain.Data.Entities
{
    public class User : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public ICollection<Review> Reviews { get; set; }
    }
}
