using EasyIn.Domain;

namespace EasyIn
{
    public class Credential : Entity
    {
        public virtual User User { get; private set; }
        public virtual Platform Platform { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public Credential() { }

        public Credential(User user, Platform platform, string username, string password)
        {
            User = user;
            Platform = platform;
            Username = username;
            Password = password;
        }

        public void Update(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
