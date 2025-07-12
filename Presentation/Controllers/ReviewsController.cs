using FoodApplication.Application;
using FoodApplication.Application.DTOs.Review;
using FoodApplication.Application.Features.Reviews.Commands;
using FoodApplication.Application.Features.Reviews.Queries;
using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel;
using FoodApplication.Presentation.ViewModel.Review;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReviewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview([FromBody] CreateReviewDTO dto)
        {
            var review = await _mediator.Send(new SubmitReviewCommand(dto));

            if (review == null)
            {
                return BadRequest(ResponseViewModel<ReviewViewModel>.Failure(ErrorCode.BadRequest, "Failed to submit review"));
            }

            var vm = review.Map<ReviewViewModel>();

            return Ok(ResponseViewModel<ReviewViewModel>.Success(vm));
        }

        [HttpGet("Item")]
        public async Task<IActionResult> GetByItem([FromQuery] GetReviewsByItemDTO dto)
        {
            var reviews = await _mediator.Send(new GetReviewsByItemQuery(dto));

            if (reviews == null)
            {
                return Ok(ResponseViewModel<List<ReviewViewModel>>.Success(new List<ReviewViewModel>()));
            }
            
            var vms = reviews.Select(r => r.Map<ReviewViewModel>()).ToList();

            return Ok(ResponseViewModel<List<ReviewViewModel>>.Success(vms));
        }
    }
}
