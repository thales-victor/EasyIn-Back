namespace EasyIn.Models
{
    public class CredentialListModel
    {
        public int Id { get; private set; }
        public string Platform { get; private set; }

        public CredentialListModel(Credential credential)
        {
            Id = credential.Id;
            Platform = credential.Platform.Name;
        }
    }
}