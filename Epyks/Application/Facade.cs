using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Epyks.Application
{
    /// <summary>
    /// Facade de la couche application
    /// </summary>
    public class Facade
    {
        private static Facade instance;
        private Membre membreCourant = null;
        private MembreDAO dao;
        private GestionnaireCommunication gestionnaireCommunication;

        /// <summary>
        /// Retourne l'instance de la facade (Singleton)
        /// </summary>
        /// <returns>l'instance de la facade</returns>
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

        /// <summary>
        /// Recherche le membre demander et le garde en mémoire
        /// </summary>
        /// <param name="username">Le nom d'utilisateur</param>
        /// <param name="password">Le mot de passe</param>
        /// <returns>vrai si le login a réussi faux sinon</returns>
        public bool Login(string username, string password)
        {
            Random rand = new Random();
            int id = rand.Next(100);
            membreCourant = dao.getMember(username, password);
           // membreCourant = new Membre(id, "m", "m", "m" + id, "m", "m", Genre.MALE, "m", new byte[1], 1);
            if (membreCourant != null)
            {
             //  gestionnaireCommunication = new GestionnaireCommunication(membreCourant);
               //gestionnaireCommunication.StartReading();
            }

            return membreCourant != null;
        }

        /// <summary>
        /// Enregistre un membre
        /// </summary>
        /// <param name="membre">le dto du membre</param>
        public void Register(MembreDTO membre)
        {
            dao.insertMember(new Membre(membre));
        }
        /// <summary>
        /// Vérifie si le nom d'utilisateur existe
        /// </summary>
        /// <param name="username">Le nom d'utilisateur</param>
        /// <returns>vrai si il existe faux sinon</returns>
        public bool UsernameExist(string username)
        {
            return dao.UsernameExist(username);
        }

        /// <summary>
        /// Vérifie si le couriel existe
        /// </summary>
        /// <param name="email">Le couriel</param>
        /// <returns>vrai si il existe faux sinon</returns>
        public bool EmailExist(string email)
        {
            return dao.EmailAdressExist(email);
        }

        /// <summary>
        /// Récupère le mot de passe à partir du email
        /// </summary>
        /// <param name="email">le couriel</param>
        /// <returns>le mot de passe</returns>
        public string recupererPassword(string email)
        {
           return dao.getPassword(email);
        }

        /// <summary>
        /// Renvoie le membre courant
        /// </summary>
        /// <returns>le dto du membre courant</returns>
        public MembreDTO getMembreCourant()
        {
            return membreCourant.getDTO();
        }

        public ArrayList getMembreListAmis(int id)
        {
            return dao.getListAmis(id);
        }

        public ArrayList getListResultat(string caracteres)
        {
            return dao.getListResultatRecherche(caracteres);
        }

        public void ajoutDunAmis(int idUtilisateur, int idAmis)
        {
            dao.ajouterUnAmis(idUtilisateur, idAmis);
        }

        public int getNewAmisId(string username)
        {
            return dao.getMemberIdByUsername(username);
        }

        public IDisposable SubscribeToStack(IObserver<Message> observer)
        {
            return membreCourant.SubscribeToStack(observer);
        }

        public void EnvoyerMessage(string messageText)
        {
            Message message = new Message(membreCourant.id, membreCourant.username, messageText);
            membreCourant.AddMessageInStack(message);
            gestionnaireCommunication.EcrireMessage(message);
        }

        public void EndThreads()
        {
            if (gestionnaireCommunication != null)
            {
                gestionnaireCommunication.EndThread();
            }
        }

        public bool verifierSiAmis(int idUtilisateur, int idAmis)
        {
            return dao.dejaAmis(idUtilisateur, idAmis);
        }

        public bool deleteAmis(int userId, int idAmis)
        {
            return dao.deleteFriend(userId, idAmis);
        }

        public bool EnvoyerPassword(string password, string emailDest)
        {
            EmailManager emailManager = EmailManager.GetInstance();
            return emailManager.EnvoyerEmail(password, emailDest);
        }

    }
}
