namespace Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
    }
}
