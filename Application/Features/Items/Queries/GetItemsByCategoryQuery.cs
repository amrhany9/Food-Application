using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Queries
{
    public record GetItemsByCategoryQuery(int CategoryId): IRequest<List<ItemDTO>>;

    public class GetItemsByCategoryHandler: IRequestHandler<GetItemsByCategoryQuery, List<ItemDTO>>
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public GetItemsByCategoryHandler(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemDTO>> Handle(GetItemsByCategoryQuery req, CancellationToken ct)
        {
            var items = await _itemRepository
                .GetByFilter(i => i.CategoryId == req.CategoryId)
                .ToListAsync(ct);

            var itemDTOs = items
                .Select(i => i.Map<ItemDTO>())
                .ToList();

            return itemDTOs;
        }
    }

}
