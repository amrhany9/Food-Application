using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Recipes.Queries
{
    public record GetAllRecipesQuery() : IRequest<List<RecipeDTO>>;

    public class GetAllRecipesHandler : IRequestHandler<GetAllRecipesQuery, List<RecipeDTO>>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public GetAllRecipesHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<RecipeDTO>> Handle(GetAllRecipesQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository
                .GetAll()
                .ToListAsync(cancellationToken);

            var recipeDTOs = recipes
                .Select(r => r.Map<RecipeDTO>())
                .ToList();

            return recipeDTOs;
        }
    }
}
