using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epyks.Application;
using System.Collections;

namespace Epyks.Coordonnateur
{
    public class CoordonateurMembreCourant
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

        public ArrayList getListAmis(int id)
        {
            return api.getMembreListAmis(id);
        }

        public ArrayList getListResultatRecherche(string caracteres)
        {
            return api.getListResultat(caracteres);
        }

        public void AjouterAmis(int idUtilisateur, int idAmis)
        {
            api.ajoutDunAmis(idUtilisateur, idAmis);
        }

        public int getIdAmis(string username)
        {
            return api.getNewAmisId(username);
        }

        public IDisposable SubscribeToStack(IObserver<Message> observer)
        {
            return api.SubscribeToStack(observer);
        }

        public void EnvoyerMessage(string messageText)
        {
            api.EvoyerMessage(messageText);
        }

        public bool VerifierSiAmis(int idUtilisateur, int idAmis)
        {
            return api.verifierSiAmis(idUtilisateur, idAmis);
        }

        public bool deleteFriend(int userId, int idAmis)
        {
            return api.deleteAmis(userId, idAmis);
        }
    }
}
