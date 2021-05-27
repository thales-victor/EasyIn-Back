using System.Collections.Generic;
using System.Linq;

namespace EasyIn.Models
{
    public class MoreThenOneCredentialError
    {
        public List<CredentialListModel> Credentials { get; private set; }

        public MoreThenOneCredentialError(List<Credential> credentials)
        {
            Credentials = credentials.Select(c => new CredentialListModel(c)).ToList();
        }
    }
}