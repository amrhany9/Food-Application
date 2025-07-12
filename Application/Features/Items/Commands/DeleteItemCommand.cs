using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Commands
{
    public record DeleteItemCommand(int Id): IRequest<ItemDTO>;

    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, ItemDTO>
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public DeleteItemHandler(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemDTO> Handle(DeleteItemCommand req, CancellationToken ct)
        {
            // TODO: Implement logic to check if item is part of an active order
            bool isPartOfActiveOrder = false; // Replace with actual check

            if (isPartOfActiveOrder)
            {
                throw new InvalidOperationException("Cannot delete: item is part of an active order.");
            }

            var entity = await _itemRepository
                .GetByIdWithTracking(req.Id)
                .FirstOrDefaultAsync(ct);

            if (entity is null)
            {
                throw new KeyNotFoundException("Item not found.");
            }
                
            await _itemRepository.SoftDeleteAsync(entity);

            var dto = entity.Map<ItemDTO>();
            return dto;
        }
    }

}
