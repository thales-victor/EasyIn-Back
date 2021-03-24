namespace EasyIn.Models
{
    public class CredentialUpdateModel
    {
        public int Id { get; set; }
        public int PlatformId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}