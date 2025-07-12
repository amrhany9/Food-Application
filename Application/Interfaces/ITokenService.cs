using FoodApplication.Domain.Data.Entities;

namespace FoodApplication.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
        bool ValidateToken(string token);
    }
}
