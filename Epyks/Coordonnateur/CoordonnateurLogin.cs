using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Coordonnateur
{
    public enum StatusLogin
    {
        INVALID_PASSWORD,
        INVALID_USERNAME
    }

    public enum StatusRegister
    {
        EXISTING_USERNAME
    }

    public class CoordonnateurLogin
    {

        public CoordonnateurLogin()
        {
        }

        public bool Login(String username, String password)
        {
            //Access a la bd
            //Retourne True si utilisateur existe
            //Retourne False si utilisateur n'existe pas
            return true;
        }

        public void Register()
        {

        }
    }

}
