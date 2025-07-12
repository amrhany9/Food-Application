using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Domain.Data.Entities
{
    public class RecipeItem : BaseEntity
    {
        public int RecipeId { get; set; }
        public int ItemId { get; set; }

        public Recipe Recipe { get; set; }
        public Item Item { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
