using Application.Interfaces;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Domain.Data.Entities;
using FoodApplication.Domain.Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Items.Commands
{
    public record UpdateItemCommand(UpdateItemDTO DTO): IRequest<ItemDTO>;

    public class UpdateItemHandler:IRequestHandler<UpdateItemCommand, ItemDTO>
    {
        private readonly IGenericRepository<Item> _itemRepo;

        public UpdateItemHandler(IGenericRepository<Item> itemRepo)
            => _itemRepo = itemRepo;

        public async Task<ItemDTO> Handle(UpdateItemCommand req,CancellationToken ct)
        {
            var item = await _itemRepo
                .GetByIdWithTracking(req.DTO.Id)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                throw new KeyNotFoundException($"Item with ID {req.DTO.Id} not found.");
            }

            var updated = req.DTO.Map<Item>();

            _itemRepo.UpdateInclude(updated,
                nameof(Item.CategoryId),
                nameof(Item.Price),
                nameof(Item.discount),
                nameof(Item.ImageURL)
            );

            var itemDTO = updated.Map<ItemDTO>();
            return itemDTO;
        }
    }
}
