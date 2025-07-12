using AutoMapper;
using FoodApplication.Application.DTOs.Item;
using FoodApplication.Application.DTOs.Recipe;
using FoodApplication.Application.Features.Items.Commands;
using FoodApplication.Application.Features.Items.Queries;
using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel;
using FoodApplication.Shared.ViewModel.Item;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var items = await _mediator.Send(new GetItemsByCategoryQuery(categoryId));

            var viewModels = _mapper.Map<List<ItemViewModel>>(items ?? new List<ItemDTO>());

            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var items = await _mediator.Send(new GetItemsByUserQuery(userId));

            var viewModels = _mapper.Map<List<ItemViewModel>>(items ?? new List<ItemDTO>());

            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpGet("Recipe/{recipeId}")]
        public async Task<IActionResult> GetByRecipe(int recipeId)
        {
            var items = await _mediator.Send(new GetItemsByRecipeQuery(recipeId));

            var viewModels = _mapper.Map<List<ItemViewModel>>(items ?? new List<ItemDTO>());

            return Ok(ResponseViewModel<List<ItemViewModel>>.Success(viewModels));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemDTO dto)
        {
            var item = await _mediator.Send(new CreateItemCommand(dto));

            if (item == null)
            {
                return BadRequest(ResponseViewModel<ItemViewModel>.Failure(ErrorCode.BadRequest, "Failed to create item"));
            }
                
            var vm = _mapper.Map<ItemViewModel>(item);

            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest(ResponseViewModel<ItemViewModel>.Failure(ErrorCode.BadRequest, "ID mismatch"));
            }
               
            var item = await _mediator.Send(new UpdateItemCommand(dto));
            if (item == null)
            {
                return BadRequest(ResponseViewModel<ItemViewModel>.Failure(ErrorCode.BadRequest, "Failed to update item"));
            }
            
            var vm = _mapper.Map<ItemViewModel>(item);

            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _mediator.Send(new DeleteItemCommand(id));
            if (item == null)
            {
                return NotFound(ResponseViewModel<ItemViewModel>.Failure(ErrorCode.NotFound, "Item not found or could not be deleted"));
            }
            
            var vm = _mapper.Map<ItemViewModel>(item);

            return Ok(ResponseViewModel<ItemViewModel>.Success(vm));
        }

        [HttpPost("AddToRecipe")]
        public async Task<IActionResult> AddToRecipe([FromBody] RecipeItemDTO dto)
        {
            var recipeItem = await _mediator.Send(new AddItemToRecipeCommand(dto));
            if (recipeItem == null)
            {
                return BadRequest(ResponseViewModel<RecipeItemViewModel>.Failure(ErrorCode.BadRequest, "Failed to add item to recipe"));
            }
            
            var vm = _mapper.Map<RecipeItemViewModel>(recipeItem);

            return Ok(ResponseViewModel<RecipeItemViewModel>.Success(vm));
        }

        [HttpDelete("RemoveFromRecipe")]
        public async Task<IActionResult> RemoveFromRecipe([FromQuery] int recipeId, [FromQuery] int itemId)
        {
            var recipeItem = await _mediator.Send(new RemoveItemFromRecipeCommand(recipeId, itemId));
            if (recipeItem == null)
            {
                return NotFound(ResponseViewModel<RecipeItemViewModel>.Failure(ErrorCode.NotFound, "Item not found in recipe or could not be removed"));
            }

            var vm = _mapper.Map<RecipeItemViewModel>(recipeItem);

            return Ok(ResponseViewModel<RecipeItemViewModel>.Success(vm));
        }
    }
}
