using EasyIn.Domain;
using System;

namespace EasyIn
{
    public class Platform : Entity
    {
        public string Name { get; private set; }
        public bool AllowIntegratedLogin { get; private set; }

        public Platform() { }

        public Platform(string description)
        {
            Name = description;
            AllowIntegratedLogin = false;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
