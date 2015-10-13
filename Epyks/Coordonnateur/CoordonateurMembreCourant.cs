using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epyks.Application;

namespace Epyks.Coordonnateur
{
    class CoordonateurMembreCourant
    {
        private Facade api;
        private MembreDTO mdtoCourant;

        private static CoordonateurMembreCourant instance;

        public static CoordonateurMembreCourant GetInstance()
        {
            if (instance == null)
            {
                instance = new CoordonateurMembreCourant();
            }
            return instance;
        }

        private CoordonateurMembreCourant()
        {
            api = Facade.GetInstance();
        }

        public MembreDTO getMembreCourant()
        {
            return mdtoCourant = api.getMembreCourant();
        }
    }
}
