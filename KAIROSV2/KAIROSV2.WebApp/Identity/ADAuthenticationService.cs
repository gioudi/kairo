using KAIROSV2.WebApp.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Threading.Tasks;

namespace KAIROSV2.WebApp.Identity
{
    public class ADAuthenticationService : IADAuthenticationService
    {
        private const string DisplayNameAttribute = "DisplayName";
        private const string SAMAccountNameAttribute = "SAMAccountName";

        private readonly LDAPConfig config;

        public ADAuthenticationService(IOptions<LDAPConfig> config)
        {
            this.config = config.Value;
        }
        public bool ValidateCredentials(string userName, string password)
        {
            bool isValid = false;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + userName, password))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = String.Format("({0}={1})", SAMAccountNameAttribute, userName);
                        searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                        searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                        var result = searcher.FindOne();
                        if (result != null)
                        {
                            var displayName = result.Properties[DisplayNameAttribute];
                            var samAccountName = result.Properties[SAMAccountNameAttribute];

                            isValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // if we get an error, it means we have a login failure.
                // Log specific exception
            }
            return isValid;
        }

        public List<UsuarioAD> GetUsers(string userName)
        {
            var usuariosResult = new List<UsuarioAD>();
            userName = userName.Replace("&", "");
            userName = userName.Replace("|", "");
            userName = userName.Replace("*", "");

            using (var searchRoot = new DirectoryEntry(config.Path, config.UserDomainName + "\\" + "xp_dolphin1", "Colombia_2021"))
            {
                using (var searcher = new DirectorySearcher(searchRoot))
                {
                   searcher.Filter = $"(&(objectCategory=person)(objectClass=user)({DisplayNameAttribute}=*{userName}*))";
                    searcher.PropertiesToLoad.Add(SAMAccountNameAttribute);
                    searcher.PropertiesToLoad.Add(DisplayNameAttribute);
                    searcher.PropertiesToLoad.Add("telephoneNumber");
                    searcher.PropertiesToLoad.Add("mail");

                    try
                    {
                        foreach (SearchResult result in searcher.FindAll())
                        {
                            if (result.Properties.Contains(SAMAccountNameAttribute))
                            {
                                var usuario = new UsuarioAD
                                {
                                    Id = (string)result.Properties[SAMAccountNameAttribute][0]
                                };

                                if (result.Properties.Contains(DisplayNameAttribute))
                                    usuario.Text = (string)result.Properties[DisplayNameAttribute][0];
                                if (result.Properties.Contains("mail"))
                                    usuario.Email = (string)result.Properties["mail"][0];
                                if (result.Properties.Contains("telephoneNumber"))
                                    usuario.Telefono = (string)result.Properties["telephoneNumber"][0];

                                usuariosResult.Add(usuario);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // if we get an error, it means we have a login failure.
                        // Log specific exception
                    }
                }
            }

            return usuariosResult;
        }
    }
}
