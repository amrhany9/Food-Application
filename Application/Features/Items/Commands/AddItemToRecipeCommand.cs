using Application.Interfaces;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace FoodApplication.Application.Features.Items.Commands
{
    public record AddItemToRecipeCommand(RecipeItemDTO recipeItemDTO) : IRequest<RecipeItemDTO>;

    public class AddItemToRecipeHandler: IRequestHandler<AddItemToRecipeCommand, RecipeItemDTO>
    {
        private readonly IGenericRepository<RecipeItem> _recipeItemRepository;

        public AddItemToRecipeHandler(IGenericRepository<RecipeItem> recipeItemRepository)
        {
            _recipeItemRepository = recipeItemRepository;
        }

        public async Task<RecipeItemDTO> Handle(AddItemToRecipeCommand req,CancellationToken ct)
        {
            if (req.recipeItemDTO == null)
            {
                throw new ArgumentNullException(nameof(req.recipeItemDTO), "Recipe item cannot be null.");
            }

            var entity = req.recipeItemDTO.Map<RecipeItem>();

            await _recipeItemRepository.AddAsync(entity);
            await _recipeItemRepository.SaveChangesAsync();

            var DTO = entity.Map<RecipeItemDTO>();

            return DTO;
        }
    }

}
