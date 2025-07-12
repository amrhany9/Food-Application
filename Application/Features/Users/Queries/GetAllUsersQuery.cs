using Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.DTOs.User;

namespace FoodApplication.Application.Features.Users.Queries
{
    public record GetAllUsersQuery : IRequest<List<UserDTO>>;

    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public GetAllUsersHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository
                .GetAll()
                .ToListAsync(cancellationToken);

            var userDTOs = users
                .Select(u => u.Map<UserDTO>())
                .ToList();

            return userDTOs;
        }
    }
}
