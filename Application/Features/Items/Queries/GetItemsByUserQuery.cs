using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Queries
{
    public record GetItemsByUserQuery(int UserId): IRequest<List<ItemDTO>>;

    public class GetItemsByUserHandler: IRequestHandler<GetItemsByUserQuery, List<ItemDTO>>
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public GetItemsByUserHandler(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<ItemDTO>> Handle(GetItemsByUserQuery req,CancellationToken ct)
        {
            var items = await _itemRepository
                .GetByFilter(i => i.UserId == req.UserId)
                .ToListAsync(ct);

            var itemDTOs = items
                .Select(i => i.Map<ItemDTO>())
                .ToList();

            return itemDTOs;
        }
    }

}
