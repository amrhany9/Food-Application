namespace FoodApplication.Application.DTOs.Recipe
{
    public class CreateRecipeDTO
    {

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public IEnumerable<string> Steps { get; set; } = Enumerable.Empty<string>();
        public decimal PrepTimeMinutes { get; set; }
        public decimal CookTimeMinutes { get; set; }
    }
}
