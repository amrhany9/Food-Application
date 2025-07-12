using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Commands
{
    public record AddItemToMenuCommand(AddItemToMenuDTO DTO) : IRequest<MenuItemDTO>;

    public class AddItemToMenuHandler : IRequestHandler<AddItemToMenuCommand, MenuItemDTO>
    {
        private readonly IGenericRepository<Item> _itemRepository;

        public AddItemToMenuHandler(IGenericRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<MenuItemDTO> Handle(AddItemToMenuCommand req, CancellationToken ct)
        {
            var item = await _itemRepository
                .GetById(req.DTO.ItemId)
                .FirstOrDefaultAsync();

            if (item is null)
            {
                throw new KeyNotFoundException("Item not found");
            }

            item.MenuId = req.DTO.MenuId;
            await _itemRepository.AddAsync(item);

            var DTO = item.Map<MenuItemDTO>();
            return DTO;
        }
    }
}
