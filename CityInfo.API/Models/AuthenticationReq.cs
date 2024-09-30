namespace CityInfo.API.Models
{
    public class AuthenticationReq
    {
        public string? UserName { get; set; } = string.Empty;
        public string? Password { get; set; }
    }
}
