using Application.Interfaces;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Menus.Queries
{
    public record GetMenusQuery() : IRequest<List<MenuDTO>>;

    public class GetMenusHandler : IRequestHandler<GetMenusQuery, List<MenuDTO>>
    {
        private readonly IGenericRepository<Menu> _menuRepository;

        public GetMenusHandler(IGenericRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<MenuDTO>> Handle(GetMenusQuery request, CancellationToken ct)
        {
            var menus = await _menuRepository.GetAll()
                .OrderBy(m => m.Title)
                .ToListAsync(ct);

            var menuDTOs = menus.Select(m => m.Map<MenuDTO>()).ToList();

            return menuDTOs;
        }
    }
}
