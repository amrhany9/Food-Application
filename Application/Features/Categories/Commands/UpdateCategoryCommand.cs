using Application.Interfaces;
using FoodApplication.Application.DTOs.Category;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Categories.Commands
{
    public record UpdateCategoryCommand(int categoryId, UpdateCategoryDTO updateCategory) : IRequest<CategoryDTO>;

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDTO>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public UpdateCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.updateCategory == null)
            {
                throw new ArgumentNullException(nameof(request.updateCategory), "Category data must be provided.");
            }

            await _categoryRepository
                .BulkUpdateAsync(x => x.Id == request.categoryId, x => x 
                    .SetProperty(x => x.Name, request.updateCategory.Name)
                    .SetProperty(x => x.Description, request.updateCategory.Description));

            var updatedCategory = await _categoryRepository
                .GetById(request.categoryId)
                .FirstOrDefaultAsync();

            if (updatedCategory == null)
            {
                throw new InvalidOperationException("Category not found after update.");
            }
                
            var categoryDTO = new CategoryDTO
            {
                Id = updatedCategory.Id,
                Name = updatedCategory.Name,
                Description = updatedCategory.Description
            };

            return categoryDTO;
        }
    }
}
