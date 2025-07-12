using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Commands
{
    public record RemoveItemFromRecipeCommand(int RecipeId, int ItemId): IRequest<RecipeItemDTO>;

    public class RemoveItemFromRecipeCommandHandler: IRequestHandler<RemoveItemFromRecipeCommand, RecipeItemDTO>
    {
        private readonly IGenericRepository<RecipeItem> _recipeItemRepository;

        public RemoveItemFromRecipeCommandHandler(IGenericRepository<RecipeItem> recipeItemRepository)
        {
            _recipeItemRepository = recipeItemRepository;
        }

        public async Task<RecipeItemDTO> Handle(RemoveItemFromRecipeCommand req,CancellationToken ct)
        {
            var recipeItem = await _recipeItemRepository
                .GetByFilter(ri => ri.RecipeId == req.RecipeId && ri.ItemId == req.ItemId)
                .FirstOrDefaultAsync(ct);

            if (recipeItem is null)
            {
                throw new KeyNotFoundException($"RecipeItem with RecipeId {req.RecipeId} and ItemId {req.ItemId} not found.");
            }

            await _recipeItemRepository.SoftDeleteAsync(recipeItem);

            var recipeItemDTO = recipeItem.Map<RecipeItemDTO>();

            return recipeItemDTO;
        }
    }
}
