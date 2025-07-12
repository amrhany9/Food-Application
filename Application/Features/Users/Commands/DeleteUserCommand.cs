using Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Users.Commands
{
    public record DeleteUserCommand(int Id) : IRequest<bool>;

    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IGenericRepository<User> _userRepository;

        public DeleteUserHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand req, CancellationToken ct)
        {
            var user = await _userRepository.GetByIdWithTracking(req.Id).FirstOrDefaultAsync(ct);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            
            await _userRepository.SoftDeleteAsync(user);

            return true;
        }
    }
}
