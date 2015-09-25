using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Epyks.Application;

using System.Net.Mail;namespace Epyks.Coordonnateur
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
        //private string messageErreur="";
        //private string erreur_1 = "Tous les champs sont requis";
        //private string erreur_2 = "Les mots de passe ne sont pas identiques";
        //private string erreur_3 = "Le formal de l'email est invalide";
        //private string emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
        //                           + "@"
        //                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        private Facade api;
        private static CoordonnateurLogin instance;

        public static CoordonnateurLogin GetInstance()
        {
            if (instance == null)
            {
                instance = new CoordonnateurLogin();
            }
            return instance;
        }

        private CoordonnateurLogin()
        {
            api = Facade.GetInstance();
        }

        public bool Login(string username, string password)
        {
           return api.Login(username, password);
        }

        private bool verifierMdp()
        {
            throw new NotImplementedException();
        }

        public bool VerifierEmail(string email)
        {
            return api.EmailExist(email);
        }

        public string recoverPassword(string email)
        {
            return api.recupererPassword(email);
        }

        public void envoyerPassword(string password, string emailDest)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("epyks_ogc@hotmail.com");
            mail.To.Add(emailDest);
            mail.Subject = "Epyks Account Recover Password";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "You password for you Epyks account is : " + password;
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("epyks_ogc@hotmail.com", "EpyksEpyks");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        public void Register(string firstname, string lastname, string email,
            string username, string password, Genre gender)
        {
            MembreDTO membre = new MembreDTO(firstname, lastname, username, password, email, gender);
            api.Register(membre);
        }

        //public void validerEntrees(string firstname, string lastname, string email, string username,
        //    string password, string confirmPassword)
        //{
        //    string message_erreur = validerChamps(firstname,lastname, email, username, password, confirmPassword);
        //    if (!message_erreur.Equals(""))
        //    {
        //        MessageBox.Show(message_erreur);
        //    }
        //    else
        //    {
        //        int nbOfRows = verifierNomUtilisateurBD(username);
        //        if (nbOfRows == 1)
        //        {
        //            MessageBox.Show("Nom d'utilisateur non disponible");
                    
        //        }
        //        else if(nbOfRows == 0)
        //        {
        //            this.Register(firstname,lastname,email,username,password,confirmPassword);
        //            MessageBox.Show("Nom d'utilisateur valide");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Nombre de lignes avec meme username incorrect");
        //        }
        //    }

        //}

        //private string validerChamps(string firstname, string lastname, string email, string username, string password, string confirmPassword)
        //{
        //    if (!(password.Equals(confirmPassword)))
        //    {
        //        messageErreur = erreur_2;
        //    }
        //    else if (!Regex.IsMatch(email, emailPattern))
        //    {
        //        messageErreur = erreur_3;
        //    }
        //    else if (username.Length == 0 || email.Length == 0
        //        || firstname.Length == 0 || lastname.Length == 0
        //    || password.Length == 0 || confirmPassword.Length == 0)
        //    {
        //        messageErreur = erreur_1;
        //    }
            
        //    return messageErreur;
        //}

        public bool verifierNomUtilisateurBD(string username)
        {
            return api.UsernameExist(username);
        }
    }
}
