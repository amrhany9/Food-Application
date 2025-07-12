using Application.Interfaces;
using FoodApplication.Application.DTOs.Category;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Categories.Queries
{
    public record GetCategoriesByUserIdQuery(int UserId) : IRequest<List<CategoryDTO>>;

    public class GetCategoriesByUserIdHandler : IRequestHandler<GetCategoriesByUserIdQuery, List<CategoryDTO>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public GetCategoriesByUserIdHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> Handle(GetCategoriesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository
                .GetByFilter(x => x.UserId == request.UserId)
                .Select(x => x.Map<CategoryDTO>())
                .ToListAsync();

            return categories;
        }
    }
}
