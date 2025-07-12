namespace FoodApplication.Shared.ViewModel.Item
{
    public class RecipeItemViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
