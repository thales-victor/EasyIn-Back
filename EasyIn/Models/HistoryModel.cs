using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Models
{
    public class HistoryModel
    {
        public int Id { get; private set; }
        public string Platform { get; private set; }
        public string Credential { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool LoggedIn { get; private set; }

        public HistoryModel(QrCodeLogin login)
        {
            Id = login.Id;
            Platform = login.Platform.Name;
            Credential = login.Credential.Username;
            CreatedAt = login.CreatedAt;
            LoggedIn = login.Removed;
        }
    }
}