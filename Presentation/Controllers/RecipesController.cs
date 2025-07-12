using AutoMapper;
using FoodApplication.Application.Features.Recipes.Commands;
using FoodApplication.Application.Features.Recipes.Queries;
using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel.Recipe;
using FoodApplication.Presentation.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FoodApplication.Application.DTOs.Recipe;

namespace FoodApplication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RecipesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllRecipesQuery());

            var viewModels = response?
                .Select(dto => _mapper.Map<RecipeListViewModel>(dto))
                .ToList() ?? new List<RecipeListViewModel>();

            return Ok(ResponseViewModel<List<RecipeListViewModel>>.Success(viewModels));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetRecipeByIdQuery(id));

            var viewModel = _mapper.Map<RecipeViewModel>(response);

            return Ok(ResponseViewModel<RecipeViewModel>.Success(viewModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecipeDTO dto)
        {
            var response = await _mediator.Send(new CreateRecipeCommand(dto));

            var viewModel = _mapper.Map<RecipeViewModel>(response);

            return Ok(ResponseViewModel<RecipeViewModel>.Success(viewModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRecipeDTO dto)
        {
            if (id != dto.Id)
                return BadRequest(ResponseViewModel<RecipeViewModel>.Failure(ErrorCode.BadRequest, "ID mismatch"));

            var response = await _mediator.Send(new UpdateRecipeCommand(dto));

            var viewModel = _mapper.Map<RecipeViewModel>(response);

            return Ok(ResponseViewModel<RecipeViewModel>.Success(viewModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteRecipeCommand(id));

            var viewModel = _mapper.Map<RecipeViewModel>(response);

            return Ok(ResponseViewModel<RecipeViewModel>.Success(viewModel));
        }
    }
}
