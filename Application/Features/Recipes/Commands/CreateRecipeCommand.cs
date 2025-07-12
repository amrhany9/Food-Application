using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace FoodApplication.Application.Features.Recipes.Commands
{
    public record CreateRecipeCommand(CreateRecipeDTO DTO) : IRequest<RecipeDTO>;

    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, RecipeDTO>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public CreateRecipeHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDTO> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = request.DTO.Map<Recipe>();

            await _recipeRepository.AddAsync(recipe);
            await _recipeRepository.SaveChangesAsync();

            var DTO = recipe.Map<RecipeDTO>();

            return DTO;
        }
    }
}
