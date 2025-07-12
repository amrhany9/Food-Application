using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Domain.Data.Entities
{
    public class Item : BaseEntity
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }
        public string ImageURL { get; set; }

        public int? MenuId { get; set; }
        public Menu Menu { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public List<RecipeItem> Recipes { get; set; }
    }
}


