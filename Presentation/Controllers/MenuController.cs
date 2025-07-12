using FoodApplication.Application;
using FoodApplication.Application.DTOs.Menu;
using FoodApplication.Application.Features.Menus.Commands;
using FoodApplication.Application.Features.Menus.Queries;
using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel;
using FoodApplication.Presentation.ViewModel.Menu;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await _mediator.Send(new GetMenusQuery());
            if (menus == null || menus.Count == 0)
            {
                return Ok(ResponseViewModel<List<MenuViewModel>>.Success(new List<MenuViewModel>()));
            }
            
            var viewModels = menus.Select(menu => menu.Map<MenuViewModel>()).ToList();

            return Ok(ResponseViewModel<List<MenuViewModel>>.Success(viewModels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuById(int id)
        {
            var menu = await _mediator.Send(new ViewMenuByIdQuery(id));
            if (menu == null)
            {
                return NotFound(ResponseViewModel<MenuDetailViewModel>.Failure(ErrorCode.NotFound, "Menu not found"));
            }

            var viewModel = menu.Map<MenuDetailViewModel>();

            return Ok(ResponseViewModel<MenuDetailViewModel>.Success(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDTO dto)
        {
            var menu = await _mediator.Send(new CreateMenuCommand(dto));
            if (menu == null)
            {
                return BadRequest(ResponseViewModel<MenuViewModel>.Failure(ErrorCode.BadRequest, "Failed to create menu"));
            }

            var viewModel = menu.Map<MenuViewModel>();

            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] EditMenuDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(ResponseViewModel<MenuViewModel>.Failure(ErrorCode.BadRequest, "ID mismatch between URL and payload."));
            }

            var menu = await _mediator.Send(new UpdateMenuCommand(dto));
            if (menu == null)
            {
                return NotFound(ResponseViewModel<MenuViewModel>.Failure(ErrorCode.NotFound, "Menu not found or could not be updated"));
            }

            var viewModel = menu.Map<MenuViewModel>();

            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _mediator.Send(new DeleteMenuCommand(id));
            if (menu == null)
            {
                return NotFound(ResponseViewModel<MenuViewModel>.Failure(ErrorCode.NotFound, "Menu not found or could not be deleted"));
            }

            var viewModel = menu.Map<MenuViewModel>();

            return Ok(ResponseViewModel<MenuViewModel>.Success(viewModel));
        }

        [HttpPost("{menuId}/items")]
        public async Task<IActionResult> AddItemToMenu(int menuId, [FromBody] AddItemToMenuDTO dto)
        {
            if (menuId != dto.MenuId)
            {
                return BadRequest(ResponseViewModel<string>.Failure(ErrorCode.BadRequest, "Menu ID in URL and payload do not match."));
            }

            var item = await _mediator.Send(new AddItemToMenuCommand(dto));
            if (item == null)
            {
                return BadRequest(ResponseViewModel<ItemForMenuViewModel>.Failure(ErrorCode.BadRequest, "Failed to add item to menu"));
            }

            var viewModel = item.Map<ItemForMenuViewModel>();

            return Ok(ResponseViewModel<ItemForMenuViewModel>.Success(viewModel));
        }

        [HttpDelete("{menuId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromMenu(int menuId, int itemId)
        {
            var item = await _mediator.Send(new RemoveItemFromMenuCommand(menuId, itemId));
            if (item == false)
            {
                return NotFound(ResponseViewModel<ItemForMenuViewModel>.Failure(ErrorCode.NotFound, "Item not found in menu or could not be removed"));
            }

            var viewModel = item.Map<ItemForMenuViewModel>();

            return Ok(ResponseViewModel<ItemForMenuViewModel>.Success(viewModel));
        }
    }
}
