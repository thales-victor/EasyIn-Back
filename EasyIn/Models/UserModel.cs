namespace EasyIn.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserModel() { }

        public UserModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.Username;
            Password = string.Empty;
        }
    }
}