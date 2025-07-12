using FoodApplication.Domain.Data.Entities;
using FoodApplication.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApplication.Application.Configurations
{
    public class RecipeConfiguration : BaseEntityConfiguration<Recipe>
    {
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Recipes)
                   .HasForeignKey(r => r.UserId);
        }
    }
}
