using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Recipes.Commands
{
    public record DeleteRecipeCommand(int Id) : IRequest<bool>;

    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeCommand, bool>
    {
        private readonly IGenericRepository<Recipe> _recipeRepository;

        public DeleteRecipeHandler(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<bool> Handle(DeleteRecipeCommand req, CancellationToken ct)
        {
            var recipe = await _recipeRepository
                .GetByIdWithTracking(req.Id)
                .FirstOrDefaultAsync();

            if (recipe is null)
            {
                return false;
            }

            await _recipeRepository.SoftDeleteAsync(recipe);

            return true;
        }
    }
}
