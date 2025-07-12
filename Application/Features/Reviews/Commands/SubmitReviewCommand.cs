using Application.Interfaces;
using FoodApplication.Application.DTOs.Review;
using FoodApplication.Domain.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodApplication.Application.Features.Reviews.Commands
{
    public record SubmitReviewCommand(CreateReviewDTO DTO) : IRequest<ReviewDTO>;

    public class SubmitReviewHandler : IRequestHandler<SubmitReviewCommand, ReviewDTO>
    {
        private readonly IGenericRepository<Review> _reviewRepository;

        public SubmitReviewHandler(IGenericRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDTO> Handle(SubmitReviewCommand req, CancellationToken ct)
        {
            var exists = await _reviewRepository
                .GetByFilter(r => r.UserId == req.DTO.UserId && r.RecipeItemId == req.DTO.RecipeItemId)
                .AnyAsync(ct);

            if (exists)
            {
                throw new InvalidOperationException("You have already submitted a review for this recipe item.");
            }
                
            var review = new Review
            {
                UserId = req.DTO.UserId,
                RecipeItemId = req.DTO.RecipeItemId,
                Rate = req.DTO.Rate,
                Comment = req.DTO.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);

            var reviewDTO = review.Map<ReviewDTO>();

            return reviewDTO;
        }
    }
}
