namespace EasyIn.Models
{
    public class PlatformModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AllowIntegratedLogin { get; set; }

        public PlatformModel() { }

        public PlatformModel(Platform platform)
        {
            Id = platform.Id;
            Name = platform.Name;
            AllowIntegratedLogin = platform.AllowIntegratedLogin;
        }
    }
}