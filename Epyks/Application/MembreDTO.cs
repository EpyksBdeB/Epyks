using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using Epyks.Coordonnateur;

namespace Epyks.Application
{
    public class MembreDTO : IDataErrorInfo
    {
        private const string ERR_USERNAME_ALREADY_EXIST = "Le nom d'utilisateur existe déjà";
        private const string ERR_USERNAME_EMPTY = "Le nom d'utilisateur est obligatoire";
        private const string ERR_PASSWORD_EMPTY = "Le mot de passe est obligatoire";
        private const string ERR_CONFIRM_PASSWORD_NOT_MATCH = "Les deux mots de passe sont différents";
        private const string ERR_FIRST_NAME_EMPTY = "Le prénom est obligatoire";
        private const string ERR_FIRST_NAME_INVALID = "Le prénom ne doit comporter que des lettres ou des -";
        private const string ERR_LAST_NAME_EMPTY = "Le nom est obligatoire";
        private const string ERR_LAST_NAME_INVALID = "Le nom est ne doit comporter que des lettres ou des '-'";
        private const string ERR_EMAIL_EMPTY = "Le couriel est obligatoire";
        private const string ERR_EMAIL_INVALID = "Le couriel n'est pas valide";

        private const string EMAIL_PATTERN = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                   + "@"
                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        private const string NAMES_PATTERN = @"^[a-zA-z \-]*$";

        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string email { get; set; }
        public Genre gender { get; set; }
        public String imgfilename { get; set; }
        public byte[] imageData { get; set; }
        public int fileSize { get; set; }


        public MembreDTO(int id, string firstName, string lastName, string username, string password,
            string email, Genre gender, String imgfilename, byte[] imageData, int fileSize)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;
            this.imgfilename = imgfilename;
            this.imageData = imageData;
            this.fileSize = fileSize;
        }

        public MembreDTO()
        {
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                CoordonnateurLogin coordonnateurLogin = CoordonnateurLogin.GetInstance();
                switch (columnName)
                {
                    case "username":
                        if (String.IsNullOrEmpty(this.username))
                        {
                            result = ERR_USERNAME_EMPTY;
                        }
                        else if (coordonnateurLogin.verifierNomUtilisateurBD(this.username))
                        {
                            result = ERR_USERNAME_ALREADY_EXIST;
                        }
                        break;
                    case "password":
                        if (string.IsNullOrEmpty(this.password))
                        {
                            result = ERR_PASSWORD_EMPTY;
                        }
                        break;
                    case "confirmPassword":
                        if (this.confirmPassword != null && !this.confirmPassword.Equals(this.password))
                        {
                            result = ERR_CONFIRM_PASSWORD_NOT_MATCH;
                        }
                        break;
                    case "firstName":
                        if (String.IsNullOrEmpty(this.firstName))
                        {
                            result = ERR_FIRST_NAME_EMPTY;
                        }
                        else if (!Regex.IsMatch(this.firstName, NAMES_PATTERN))
                        {
                            result = ERR_FIRST_NAME_INVALID;
                        }
                        break;
                    case "lastName":
                        if (String.IsNullOrEmpty(this.lastName))
                        {
                            result = ERR_LAST_NAME_EMPTY;
                        }
                        else if (!Regex.IsMatch(this.lastName, NAMES_PATTERN))
                        {
                            result = ERR_LAST_NAME_INVALID;
                        }
                        break;
                    case "email":
                        if (String.IsNullOrEmpty(this.email))
                        {
                            result = ERR_EMAIL_EMPTY;
                        }
                        else if (!Regex.IsMatch(this.email, EMAIL_PATTERN))
                        {
                            result = ERR_EMAIL_INVALID;
                        }
                        break;
                    default:
                        result = null;
                        break;
                }
                return result;
            }
        }
    }
}
