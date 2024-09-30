namespace CityInfo.API.Models
{
    public class CityInfoUser
    {
        public CityInfoUser(int userId, string userName, string firstName, string lastName, string city, string role)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Role = role;
        }

        public int UserId {  get; set; }
        public string UserName { get; set; }
        public string Password {  get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string City {  get; set; }
        public string Role { get; set; }

    }
}
