namespace EasyIn.Models
{
    public class AuthenticationListModel
    {
        public string Platform { get; set; }

        public AuthenticationListModel(Authentication authentication)
        {
            Platform = authentication.Platform.Name;
        }
    }
}