using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epyks.Application;
using System.Collections;
using System.Windows.Media;

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

        public List<MembreDTO> GetListAmis(int id)
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

        public IDisposable SubscribeToStack(IObserver<Message> observer, int amisId)
        {
            return api.SubscribeToStack(observer, amisId);
        }

        public void EnvoyerMessage(string messageText, int amisId)
        {
            api.EnvoyerMessage(messageText, amisId);
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

        public void UpdatePassword(int id, string password)
        {
            api.UpdatePassword(id, password);
        }

        public ImageSource retreiveImageProfile(int id){
            return api.retreiveImageProfile(id);
        }
    }
}
