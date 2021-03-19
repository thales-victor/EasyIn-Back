namespace EasyIn.Models
{
    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsTemporaryPassword { get; set; }

        public UserModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.Username;
            IsTemporaryPassword = user.IsTemporaryPassword;
        }
    }
}