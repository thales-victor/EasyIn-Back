using EasyIn.Domain;
using System;

namespace EasyIn
{
    public class Platform : Entity
    {
        public string Name { get; private set; }

        public Platform() { }

        public Platform(string description)
        {
            Name = description;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
