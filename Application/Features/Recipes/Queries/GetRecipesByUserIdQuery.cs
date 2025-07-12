using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Recipes.Queries
{
    public record GetRecipesByUserIdQuery(int userId) : IRequest<List<RecipeDTO>>;

    public class GetRecipesByUserIdHandler : IRequestHandler<GetRecipesByUserIdQuery, List<RecipeDTO>>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public GetRecipesByUserIdHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<List<RecipeDTO>> Handle(GetRecipesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var recipes = await _recipeRepository
                .GetByFilter(x => x.UserId == request.userId)
                .Select(r => r.Map<RecipeDTO>())
                .ToListAsync(cancellationToken);

            return recipes;
        }
    }
}
