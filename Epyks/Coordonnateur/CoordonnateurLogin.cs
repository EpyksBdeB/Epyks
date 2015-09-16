﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private String messageErreur="";
        private String erreur_1 = "Tous les champs sont requis";
        private String erreur_2 = "Les mots de passe ne sont pas identiques";
        private String erreur_3 = "Le formal de l'email est invalide";
        private String emailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        public CoordonnateurLogin()
        {
        }

        public bool Login(String username, String password)
        {
            nouveauMembreDAO = new MembreDAO();
            bool usernameUnique = verifierUserBD(username);
            bool mdpValide = verifierMdp();
            //nouveauMembreDao.insertMember();
            //Access a la bd
            //Retourne True si utilisateur existe
            //Retourne False si utilisateur n'existe pas
            return true;
        }

        private bool verifierMdp()
        {
            throw new NotImplementedException();
        }

        private bool verifierUserBD(String username)
        {
            //bool existe = nouveauMembreDAO.trouverMember(1);
            Membre nouveauMembre = nouveauMembreDAO.getMember(username);
            if (nouveauMembre == null)
            {
                MessageBox.Show("Utilisateur non présent dans la BD");
                // Utilisateur non présent dans la BD
            }
            return true;
        }

        public void Register(String firstname, String lastname, String email,
            String username, String password, String confirmPassword)
        {
            // Appelle methode register dans facade
        }

        public void validerEntrees(string firstname, string lastname, string email, string username,
            string password, string confirmPassword)
        {
            String message_erreur = validerChamps(firstname,lastname, email, username, password, confirmPassword);
            if (!message_erreur.Equals(""))
            {
                MessageBox.Show(message_erreur);
            }
            else
            {
                int nbOfRows = verifierNomUtilisateurBD(username);
                if (nbOfRows == 1)
                {
                    MessageBox.Show("Nom d'utilisateur non disponible");
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur valide");
                }
            }

        }

        private String validerChamps(string firstname, string lastname, string email, string username, string password, string confirmPassword)
        {
            if (!(password.Equals(confirmPassword)))
            {
                messageErreur = erreur_2;
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                messageErreur = erreur_3;
            }
            else if (username.Length == 0 || email.Length == 0
                || firstname.Length == 0 || lastname.Length == 0
            || password.Length == 0 || confirmPassword.Length == 0)
            {
                messageErreur = erreur_1;
            }
            
            return messageErreur;
        }

        public int verifierNomUtilisateurBD(string username)
        {
            nouveauMembreDAO = new MembreDAO();
             int nbOfRows = nouveauMembreDAO.trouverUsername(username);
            return nbOfRows;
        }
    }

}
