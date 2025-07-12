namespace FoodApplication.Application.DTOs.Review
{
    public class GetReviewsByItemDTO
    {
        public int RecipeItemId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
