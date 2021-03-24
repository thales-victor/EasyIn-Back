namespace EasyIn.Models
{
    public class CredentialModel
    {
        public PlatformModel Platform { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CredentialModel(Credential credential)
        {
            Platform = new PlatformModel(credential.Platform);
            Username = credential.Username;
            Password = string.Empty;
        }
    }
}