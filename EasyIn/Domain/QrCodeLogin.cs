using EasyIn.Domain;
using System;

namespace EasyIn
{
    public class QrCodeLogin : Entity
    {
        public virtual Credential Credential { get; private set; }
        public virtual Platform Platform { get; private set; }
        public string BrowserToken { get; private set; }
        public DateTime ExpirationDate { get; set; }

        public QrCodeLogin() { }

        public QrCodeLogin(Credential credential, string browserToken)
        {
            Credential = credential;
            Platform = credential.Platform;
            BrowserToken = browserToken;
            ExpirationDate = DateTime.UtcNow.AddMinutes(5);
        }
    }
}
