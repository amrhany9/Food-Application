using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Queries
{
    public record ViewMenuByIdQuery(int MenuId) : IRequest<MenuDetailDTO>;

    public class ViewMenuByIdHandler : IRequestHandler<ViewMenuByIdQuery, MenuDetailDTO>
    {
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<Item> _itemRepository;

        public ViewMenuByIdHandler(IGenericRepository<Menu> menuRepository, IGenericRepository<Item> itemRepository)
        {
            _menuRepository = menuRepository;
            _itemRepository = itemRepository;
        }

        public async Task<MenuDetailDTO> Handle(ViewMenuByIdQuery request, CancellationToken ct)
        {
            var menu = await _menuRepository
                .GetByFilter(m => m.Id == request.MenuId)
                .FirstOrDefaultAsync(ct);

            if (menu is null)
            {
                throw new KeyNotFoundException($"Menu with ID {request.MenuId} not found.");
            }
                
            var itemDTOs = new List<MenuItemDTO>();

            foreach (var item in menu.Items)
            {
                var dbItem = await _itemRepository
                    .GetById(item.Id)
                    .FirstOrDefaultAsync();

                if (dbItem != null)
                {
                    itemDTOs.Add(dbItem.Map<MenuItemDTO>());
                }
            }

            var menuDetails = menu.Map<MenuDetailDTO>();

            menuDetails.Items = itemDTOs;

            return menuDetails;
        }
    }
}
