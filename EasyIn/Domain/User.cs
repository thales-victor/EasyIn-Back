using EasyIn.Domain;
using System;
using System.Collections.Generic;

namespace EasyIn
{
    public class User : Entity
    {
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsTemporaryPassword { get; private set; }

        public virtual List<Credential> Credentials { get; private set; } = new List<Credential>();

        public User() { }

        public User(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }

        public void Update(string email, string username, string password)
        {
            Email = email;
            Username = username;

            if (IsChangingPassword(password))
            {
                Password = password;
                IsTemporaryPassword = false;
            }
        }

        public void HidePassword()
        {
            Password = string.Empty;
        }

        private bool IsChangingPassword(string password)
        {
            return !string.IsNullOrEmpty(password) && Password != password;
        }

        public void SetTemporaryPassword(string password)
        {
            Password = password;
            IsTemporaryPassword = true;
        }
    }
}
