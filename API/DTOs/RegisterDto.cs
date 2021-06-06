namespace API.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string KnownAs { get; set; }
        public string UserBio { get; set; }
        public string PhotoUrl { get; set; }
    }
}