namespace JwtApp.Models
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? EmailAdress { get; set; }
        public string? Role { get; set; } = string.Empty;
        public string? Surname { get; set; } = string.Empty;
        public string? GivenName { get; set; }
    }
}
