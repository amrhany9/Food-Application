using Application.DTOs.Category;
using Application.Interfaces;
using FoodApplication.Application.DTOs.Category;
using FoodApplication.Domain.Data.Entities;
using MediatR;

namespace FoodApplication.Application.Features.Categories.Commands
{
    public record CreateCategoryCommand(CreateCategoryDTO createCategory) : IRequest<CategoryDTO>;

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDTO>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CreateCategoryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDTO> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.createCategory == null)
            {
                throw new ArgumentNullException(nameof(request.createCategory), "Category data must be provided.");
            }

            var category = request.createCategory.Map<Category>();

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return categoryDTO;
        }
    }
}
