using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Recipes.Commands
{
    public record UpdateRecipeCommand(UpdateRecipeDTO DTO) : IRequest<RecipeDTO>;

    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, RecipeDTO>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public UpdateRecipeHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDTO> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository
                .GetByIdWithTracking(request.DTO.Id)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {request.DTO.Id} not found.");
            }

            var updated = request.DTO.Map<Recipe>();

            _recipeRepository.UpdateInclude(updated,
                nameof(Recipe.Title),
                nameof(Recipe.Description),
                nameof(Recipe.ImageUrl),
                nameof(Recipe.Steps),
                nameof(Recipe.PrepTime),
                nameof(Recipe.CookTime)
            );

            var recipeDTO = updated.Map<RecipeDTO>();
            return recipeDTO;
        }
    }
}
