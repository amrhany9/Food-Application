using Application.DTOs.Auth;
using Application.Interfaces;
using FoodApplication.Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands
{
    public record LoginCommand(LoginDTO loginDTO) : IRequest<string>;

    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(
            ITokenService tokenService,
            IGenericRepository<User> userRepository,
            IPasswordHasher passwordHasher)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilter(u => 
                u.Email == request.loginDTO.Email && 
                u.IsActive)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null || !_passwordHasher.VerifyPassword(request.loginDTO.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return _tokenService.GenerateJwtToken(user);
        }
    }
}
