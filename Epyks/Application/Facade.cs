using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Epyks.Application
{
    public class Facade
    {
        private static Facade instance;

        private Membre membreCourant = null;
        private MembreDAO dao;

        public static Facade GetInstance()
        {
            if (instance == null)
            {
                instance = new Facade();
            }
            return instance;
        }

        private Facade()
        {
            dao = MembreDAO.GetInstance();
        }

        public bool Login(string username, string password)
        {
            membreCourant = dao.getMember(username, password);
            return membreCourant != null;
        }

        public void Register(MembreDTO membre)
        {
            dao.insertMember(new Membre(membre));
        }

        public bool UsernameExist(string username)
        {
            return dao.UsernameExist(username);
        }

        public bool EmailExist(string email)
        {
            return dao.EmailAdressExist(email);
        }

        public string recupererPassword(string email)
        {
           return dao.getPassword(email);
        }

        public MembreDTO getMembreCourant()
        {
            membreCourant = new Membre();
            return membreCourant.getDTO();
        }
    }
}
