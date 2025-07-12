using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Commands
{
    public record DeleteMenuCommand(int Id): IRequest<MenuDTO>;

    public class DeleteMenuHandler : IRequestHandler<DeleteMenuCommand, MenuDTO>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public DeleteMenuHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuDTO> Handle(DeleteMenuCommand req, CancellationToken ct)
        {
            // Implement logic to check if the menu is in active use.
            // For example, you might check if there are any active orders or references.
            bool isMenuInActiveUse = false; // TODO: Replace with actual logic

            if (isMenuInActiveUse)
            {
                throw new InvalidOperationException("Cannot delete: menu is in active use.");
            }

            var menu = await _menuRepository.GetByIdWithTracking(req.Id).FirstOrDefaultAsync(ct);

            if (menu is null)
            {
                throw new KeyNotFoundException("Menu not found.");
            }

            await _menuRepository.SoftDeleteAsync(menu);

            var menuDTO = menu.Map<MenuDTO>();
            return menuDTO;
        }
    }

}
