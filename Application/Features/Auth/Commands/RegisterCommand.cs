using Application.DTOs.Auth;
using Application.Interfaces;
using FoodApplication.Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace Application.Features.Auth.Commands
{
    public record RegisterCommand(RegisterDTO registerDTO) : IRequest<string>;

    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public RegisterHandler(
            IGenericRepository<User> userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordHasher.HashPassword(request.registerDTO.Password);

            var user = new User
            {
                RoleId = request.registerDTO.RoleId,
                Username = request.registerDTO.Username,
                Password = hashedPassword,
                Email = request.registerDTO.Email,
                Phone = request.registerDTO.Phone,
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _tokenService.GenerateJwtToken(user);
        }
    }
}
