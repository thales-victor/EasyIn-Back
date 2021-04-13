using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Models
{
    public class UserCreateModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}