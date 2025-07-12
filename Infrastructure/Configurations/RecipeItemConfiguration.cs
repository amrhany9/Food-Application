using FoodApplication.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Infrastructure.Configurations
{
    public class RecipeItemConfiguration : BaseEntityConfiguration<RecipeItem>
    {
        public override void Configure(EntityTypeBuilder<RecipeItem> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.RecipeId);

            builder.HasOne(x => x.Item)
                .WithMany(x => x.Recipes)
                .HasForeignKey(x => x.ItemId);
        }
    }
}
