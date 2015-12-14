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

        public MembreDTO GetMembreCourant()
        {
            return mdtoCourant = api.GetMembreCourant();
        }

        public ArrayList GetListAmis(int id)
        {
            return api.GetMembreListAmis(id);
        }

        public ArrayList GetListResultatRecherche(string caracteres)
        {
            return api.GetListResultat(caracteres);
        }

        public void AjouterAmis(int idUtilisateur, int idAmis)
        {
            api.AjoutDunAmis(idUtilisateur, idAmis);
        }

        public int GetIdAmis(string username)
        {
            return api.GetNewAmisId(username);
        }

        public IDisposable SubscribeToStack(IObserver<Message> observer)
        {
            return api.SubscribeToStack(observer);
        }

        public void EnvoyerMessage(string messageText)
        {
            api.EnvoyerMessage(messageText);
        }

        public bool VerifierSiAmis(int idUtilisateur, int idAmis)
        {
            return api.VerifierSiAmis(idUtilisateur, idAmis);
        }

        public bool DeleteFriend(int userId, int idAmis)
        {
            return api.DeleteAmis(userId, idAmis);
        }

        public bool verifierInput(string messagePrive)
        {
            return (messagePrive.Length < 5 || messagePrive.Length > 140);
        }

        public void ModifierInfosAPartirProfil(string nom, string prenom, string email, string notel, int id)
        {
            api.ModifierInfosAPartirProfil(nom, prenom, email, notel, id);
        }

        public void DeleteMember(int id){
            api.DeleteMember(id);
        }
    }
}
