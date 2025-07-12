using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace FoodApplication.Application.Features.Menus.Commands
{
    public record CreateMenuCommand(CreateMenuDTO DTO): IRequest<MenuDTO>;

    public class CreateMenuHandler: IRequestHandler<CreateMenuCommand, MenuDTO>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public CreateMenuHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<MenuDTO> Handle(CreateMenuCommand req,CancellationToken ct)
        {
            if (req.DTO == null)
            {
                throw new ArgumentNullException(nameof(req.DTO), "Menu DTO cannot be null");
            }

            var entity = req.DTO.Map<Menu>();

            await _menuRepository.AddAsync(entity);
            await _menuRepository.SaveChangesAsync();

            var menuDTO = entity.Map<MenuDTO>();
            return menuDTO;
        }
    }
}
