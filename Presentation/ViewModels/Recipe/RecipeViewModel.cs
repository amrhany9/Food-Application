namespace FoodApplication.Presentation.ViewModel.Recipe
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string? ImageUrl { get; set; }
        public IEnumerable<string> Steps { get; set; } = Enumerable.Empty<string>();
        public string PrepTime { get; set; } = default!;
        public string CookTime { get; set; } = default!;
    }
}
