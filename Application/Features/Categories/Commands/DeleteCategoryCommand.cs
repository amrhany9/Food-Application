using Application.Interfaces;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Categories.Commands
{
    public record DeleteCategoryCommand(int categoryId) : IRequest<bool>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public DeleteCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _categoryRepository
                .GetByFilter(x => x.Id == request.categoryId)
                .AnyAsync();

            if (!categoryExists)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var category = await _categoryRepository
                .GetByFilter(x => x.Id == request.categoryId)
                .FirstOrDefaultAsync();

            await _categoryRepository.SoftDeleteAsync(category);

            return true;
        }
    }
}
