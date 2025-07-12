using Application.Features.Auth.Commands;
using FoodApplication.Domain.Data.Enums;
using FoodApplication.Presentation.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodApplication.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var token = await _mediator.Send(command);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(ResponseViewModel<string>.Failure(ErrorCode.BadRequest, "Registration failed"));
            }
                
            return Ok(ResponseViewModel<string>.Success(token));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var token = await _mediator.Send(command);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(ResponseViewModel<string>.Failure(ErrorCode.Unauthorized, "Invalid credentials"));
            }
            
            return Ok(ResponseViewModel<string>.Success(token));
        }
    }
}
