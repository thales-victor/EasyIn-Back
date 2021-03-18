namespace EasyIn.Models
{
    public class AuthenticationListModel
    {
        public int Id { get; private set; }
        public string Platform { get; private set; }

        public AuthenticationListModel(Authentication authentication)
        {
            Id = authentication.Id;
            Platform = authentication.Platform.Name;
        }
    }
}