namespace FoodApplication.Application.DTOs.Review
{
    public class CreateReviewDTO
    {
        public int UserId { get; set; }
        public int RecipeItemId { get; set; }
        public int Rate { get; set; }             
        public string Comment { get; set; }
    }
}
