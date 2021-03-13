using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Models
{
    public class AuthenticationUpdateModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlatformId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}