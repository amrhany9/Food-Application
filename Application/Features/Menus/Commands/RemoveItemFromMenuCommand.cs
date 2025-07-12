using Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Commands
{
    public record RemoveItemFromMenuCommand(int MenuId, int ItemId) : IRequest<bool>;

    public class RemoveItemFromMenuHandler : IRequestHandler<RemoveItemFromMenuCommand, bool>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public RemoveItemFromMenuHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<bool> Handle(RemoveItemFromMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository
                .GetByIdWithTracking(request.MenuId)
                .FirstOrDefaultAsync(cancellationToken);

            if (menu == null)
            {
                throw new KeyNotFoundException("Menu not found.");
            }

            var item = menu.Items.FirstOrDefault(i => i.Id == request.ItemId);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found in the menu.");
            }

            menu.Items.Remove(item);
            await _menuRepository.UpdateAsync(menu);

            return true;
        }
    }
}
