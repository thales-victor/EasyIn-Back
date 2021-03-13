namespace EasyIn.Models
{
    public class AuthenticationModel
    {
        public UserModel User { get; set; }
        public PlatformModel Platform { get; set; }
        public string Password { get; set; }

        public AuthenticationModel(Authentication authentication)
        {
            User = new UserModel(authentication.User);
            Platform = new PlatformModel(authentication.Platform);
            Password = string.Empty;
        }
    }
}