using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Queries
{
    public record GetItemsByRecipeQuery(int RecipeId): IRequest<List<ItemDTO>>;

    public class GetItemsByRecipeHandler: IRequestHandler<GetItemsByRecipeQuery, List<ItemDTO>>
    {
        private readonly IGenericRepository<RecipeItem> _recipeItemRepository;
        private readonly IGenericRepository<Item> _itemRepository;

        public GetItemsByRecipeHandler(
            IGenericRepository<RecipeItem> recipeItemRepository,
            IGenericRepository<Item> itemRepository)
        {
            _recipeItemRepository = recipeItemRepository;
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemDTO>> Handle(GetItemsByRecipeQuery req,CancellationToken ct)
        {
            var links = await _recipeItemRepository.GetAll()
                .Where(ri => ri.RecipeId == req.RecipeId)
                .ToListAsync(ct);

            var items = new List<ItemDTO>();

            foreach (var link in links)
            {
                var item = await _itemRepository
                    .GetById(link.ItemId)
                    .FirstOrDefaultAsync();

                if (item != null)
                {
                    items.Add(item.Map<ItemDTO>());
                }
            }

            return items;
        }
    }
}
