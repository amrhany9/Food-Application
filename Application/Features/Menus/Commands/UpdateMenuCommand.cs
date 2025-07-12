using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using FoodApplication.Domain.Data.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Commands
{
    public record UpdateMenuCommand(EditMenuDTO DTO): IRequest<MenuDTO>;

    public class UpdateMenuHandler: IRequestHandler<UpdateMenuCommand, MenuDTO>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public UpdateMenuHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuDTO> Handle(UpdateMenuCommand req,CancellationToken ct)
        {
            var existing = await _menuRepository
                .GetByIdWithTracking(req.DTO.Id)
                .FirstOrDefaultAsync();

            if (existing is null)
            {
                throw new KeyNotFoundException($"Menu with ID {req.DTO.Id} not found.");
            }

            _menuRepository.UpdateInclude(
                req.DTO.Map<Menu>(),
                nameof(Menu.Title),
                nameof(Menu.Description),
                nameof(Menu.ImageUrl),
                nameof(Menu.CategoryId)
            );

            var menuDTO = existing.Map<MenuDTO>();

            return menuDTO;
        }
    }
}
