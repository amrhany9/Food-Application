using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Domain.Data.Entities
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public User User { get; set; }
        public Recipe Recipe { get; set; }
    }
}
