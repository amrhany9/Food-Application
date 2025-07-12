using Application.Interfaces;
using FoodApplication.Application.DTOs.Review;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Reviews.Queries
{
    public record GetReviewsByItemQuery(GetReviewsByItemDTO DTO): IRequest<List<ReviewDTO>>;

    public class GetReviewsByItemHandler : IRequestHandler<GetReviewsByItemQuery, List<ReviewDTO>>
    {
        private readonly IGenericRepository<Review> _reviewRepository;

        public GetReviewsByItemHandler(IGenericRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewDTO>> Handle(GetReviewsByItemQuery req,CancellationToken ct)
        {
            var query = _reviewRepository
                .GetByFilter(r => r.RecipeItemId == req.DTO.RecipeItemId && !r.IsDeleted)
                .OrderByDescending(r => r.CreatedAt);

            var list = await query
                .Skip((req.DTO.PageIndex - 1) * req.DTO.PageSize)
                .Take(req.DTO.PageSize)
                .ToListAsync(ct);

            var DTOs = list
                .Select(r => r.Map<ReviewDTO>())
                .ToList();

            return DTOs;
        }
    }

}
