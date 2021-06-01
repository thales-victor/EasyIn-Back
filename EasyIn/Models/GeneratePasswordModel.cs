namespace EasyIn.Models
{
    public class GeneratePasswordModel
    {
        public int PasswordSize { get; set; }
        public bool UseLowercase { get; set; }
        public bool UseUppercase { get; set; }
        public bool UseNumbers { get; set; }
        public bool UseSpecial { get; set; }
    }

    public class GeneratePasswordResultModel 
    {
        public string Password { get; private set; }

        public GeneratePasswordResultModel(string password)
        {
            Password = password;
        }
    }

}