namespace FoodApplication.Application.DTOs.Menu
{
    public class CreateMenuDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MealTime { get; set; } 
        public List<int> ItemIds { get; set; } 
    }
}
