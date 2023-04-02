using KAIROSV2.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity
{
    public interface IADAuthenticationService
    {
        bool ValidateCredentials(string userName, string password);
        List<UsuarioAD> GetUsers(string userName);
    }
}
