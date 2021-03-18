using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Services.Models
{
    public class EmailOptions
    {
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public string HostUsername { get; set; }
        public string HostPassword { get; set; }
        public string SenderName { get; set; }
        public SecureSocketOptions HostSecureSocketOptions { get; set; }

        public EmailOptions()
        {
            HostSecureSocketOptions = SecureSocketOptions.Auto;
        }
    }
}
