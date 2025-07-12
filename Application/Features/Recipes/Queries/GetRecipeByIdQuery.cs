using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Recipes.Queries
{
    public record GetRecipeByIdQuery(int Id) : IRequest<RecipeDTO>;

    public class GetRecipeByIdHandler : IRequestHandler<GetRecipeByIdQuery, RecipeDTO>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public GetRecipeByIdHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeDTO> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeRepository
                .GetById(request.Id)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {request.Id} not found.");
            }

            var recipeDTO = recipe.Map<RecipeDTO>();
            return recipeDTO;
        }
    }
}
