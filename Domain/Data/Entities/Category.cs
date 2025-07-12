using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Domain.Data.Entities
{
    public class Category : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

    }
}
