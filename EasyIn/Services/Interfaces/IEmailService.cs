using EasyIn.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html);
    }
}
