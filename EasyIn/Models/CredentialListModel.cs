namespace EasyIn.Models
{
    public class CredentialListModel
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Platform { get; private set; }

        public CredentialListModel(Credential credential)
        {
            Id = credential.Id;
            Username = credential.Username;
            Platform = credential.Platform.Name;
        }
    }
}