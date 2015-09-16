using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Epyks.Application;

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
        public MembreDAO nouveauMembreDAO;

        public CoordonnateurLogin()
        {
        }

        public bool Login(String username, String password)
        {
            nouveauMembreDAO = new MembreDAO();
            verifierUserBD();
            //nouveauMembreDao.insertMember();
            //Access a la bd
            //Retourne True si utilisateur existe
            //Retourne False si utilisateur n'existe pas
            return true;
        }

        private bool verifierUserBD()
        {
            //bool existe = nouveauMembreDAO.trouverMember(1);
            Membre nouveauMembre = nouveauMembreDAO.getMember(1);
            if (nouveauMembre == null)
            {
                MessageBox.Show("Utilisateur non présent dans la BD");
                // Utilisateur non présent dans la BD
            }
            return true;
        }

        public void Register()
        {
            // Appelle methode register dans facade
        }
    }

}
