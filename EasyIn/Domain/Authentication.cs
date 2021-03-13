using EasyIn.Domain;

namespace EasyIn
{
    public class Authentication : Entity
    {
        public virtual User User { get; private set; }
        public virtual Platform Platform { get; private set; }
        public string Password { get; private set; }

        public Authentication() { }

        public Authentication(User user, Platform platform, string password)
        {
            User = user;
            Platform = platform;
            Password = password;
        }

        public void Update(string password)
        {
            Password = password;
        }
    }
}
