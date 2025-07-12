namespace FoodApplication.Domain.Data.Entities
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public int RecipeItemId { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public RecipeItem RecipeItem { get; set; }
    }
}
