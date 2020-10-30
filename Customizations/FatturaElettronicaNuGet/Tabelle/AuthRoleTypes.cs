using FatturaElettronica.Tabelle;

namespace AspNET.Customizations.FatturaElettronicaNuGet.Tabelle
{
    public class AuthRoleTypes : Tabella
    {
        public override Tabella[] List
        {
            get
            {
                return new Tabella[] {
                    new AuthRoleTypes { Codice = "admin", Nome = "admin" },
                    new AuthRoleTypes { Codice = "user", Nome = "user" }
                };
            }
        }
    }
}
