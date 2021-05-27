namespace EasyIn.Models
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginRequestModel() { }

        public LoginRequestModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class LoginResultModel
    {
        public UserModel User { get; private set; }
        public string Token { get; private set; }

        public LoginResultModel(User user, string token)
        {
            User = new UserModel(user);
            Token = token;
        }
    }

    public class UserModel
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public bool IsTemporaryPassword { get; private set; }

        public UserModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.Username;
            IsTemporaryPassword = user.IsTemporaryPassword;
        }
    }
}