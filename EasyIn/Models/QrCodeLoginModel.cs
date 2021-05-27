namespace EasyIn.Models
{
    public class QrCodeLoginModel
    {
        public int PlatformId { get; set; }
        public int CredentialId { get; set; }
        public string BrowserToken { get; set; }
    }

    public class QrCodeLoginResultModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public QrCodeLoginResultModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}