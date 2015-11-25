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
            membreCourant = dao.GetMember(username, password);
           // membreCourant = new Membre(id, "m", "m", "m" + id, "m", "m", Genre.MALE, "m", new byte[1], 1);
            if (membreCourant != null)
            {
               gestionnaireCommunication = new GestionnaireCommunication(membreCourant);
               gestionnaireCommunication.StartReading();
            }

            return membreCourant != null;
        }

        /// <summary>
        /// Enregistre un membre
        /// </summary>
        /// <param name="membre">le dto du membre</param>
        public void Register(MembreDTO membre)
        {
            dao.InsertMember(new Membre(membre));
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
        public string RecupererPassword(string email)
        {
           return dao.GetPassword(email);
        }

        /// <summary>
        /// Renvoie le membre courant
        /// </summary>
        /// <returns>le dto du membre courant</returns>
        public MembreDTO GetMembreCourant()
        {
            return membreCourant.GetDTO();
        }

        /// <summary>
        /// Récupère la liste d'amis de l'utilisateur en cours
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retourne la list d'amis de l'utilisateur en cours</returns>
        public ArrayList GetMembreListAmis(int id)
        {
            return dao.GetListAmis(id);
        }

        /// <summary>
        /// Récupère la liste d'utilisateur correspondant au string de recherche
        /// </summary>
        /// <param name="caracteres"></param>
        /// <returns>Retourne la liste des contacts résultant</returns>
        public ArrayList GetListResultat(string caracteres)
        {
            return dao.GetListResultatRecherche(caracteres);
        }

        /// <summary>
        /// Ajout d'un amis dans la liste d'amis de l'utilisateur en cours
        /// </summary>
        /// <param name="idUtilisateur"></param>
        /// <param name="idAmis"></param>
        public void AjoutDunAmis(int idUtilisateur, int idAmis)
        {
            dao.AjouterUnAmis(idUtilisateur, idAmis);
        }

        /// <summary>
        /// Récupère l'id d'un utilisateur par son username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Retourne l'id de l'utilisateur</returns>
        public int GetNewAmisId(string username)
        {
            return dao.GetMemberIdByUsername(username);
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

        /// <summary>
        /// Vérifier si l'utilisateur en cours est amis avec un autre utilisateur 
        /// </summary>
        /// <param name="idUtilisateur"></param>
        /// <param name="idAmis"></param>
        /// <returns>Retourne True si amis, False si non</returns>
        public bool VerifierSiAmis(int idUtilisateur, int idAmis)
        {
            return dao.DejaAmis(idUtilisateur, idAmis);
        }

        /// <summary>
        /// Supprimer un utilisateur de la liste d'amis de l'utilisateur en cours
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="idAmis"></param>
        /// <returns>Retourne True si delete complèté, False si non</returns>
        public bool DeleteAmis(int userId, int idAmis)
        {
            return dao.DeleteFriend(userId, idAmis);
        }
        
        /// <summary>
        /// Appel EmailManager pour envoyer par email le password de l'utilisateur correspondant au email
        /// </summary>
        /// <param name="password"></param>
        /// <param name="emailDest"></param>
        /// <returns>Retourne True si email envoyé, False si non</returns>
        public bool EnvoyerPassword(string password, string emailDest)
        {
            EmailManager emailManager = EmailManager.GetInstance();
            return emailManager.EnvoyerEmail(password, emailDest);
        }

    }
}
