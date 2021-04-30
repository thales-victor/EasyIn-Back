namespace EasyIn.Models
{
    public class CredentialModel
    {
        public int Id { get; private set; }
        public PlatformModel Platform { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public CredentialModel(Credential credential, bool showPassword = false)
        {
            Id = credential.Id;
            Username = credential.Username;
            Password = showPassword ? credential.Password : string.Empty;
            Platform = new PlatformModel(credential.Platform);
        }
    }
}