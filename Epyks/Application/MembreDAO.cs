using System;
using System.Collections.Generic;
using MySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace Epyks.Application
{
    internal class MembreDAO : MembreDAOInterface
    {
        private MySql.Data.MySqlClient.MySqlConnection connection = null;
        private MySqlCommand command;
        private string myConnectionstring = null;
        private string adresseConnection = Properties.Settings.Default.Server_address;
        private static MembreDAO instance = new MembreDAO();
        private string passwordCrypte = null;
        private String passwordDecrypte = null;

        internal static MembreDAO GetInstance()
        {
            return instance;
        }

        private MembreDAO()
        {
            try
            {
                initializeDatabase();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Initialisation de la base de donnée
        /// </summary>
        private void initializeDatabase()
        {
            //myConnectionstring = "server=localhost;uid=melissa_07;" + "pwd=Cartigan0;database=test;";

            //Olivier: ma connectionString pour chez moi!
            //myConnectionstring = "server = localhost; user id = FakeUser; password = FakePass; database = epyks; persistsecurityinfo = True";

           myConnectionstring = "server=aegaur.ddns.net;uid=epyks;pwd=gr007,,;database=epyks;port=8080;";
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = myConnectionstring;
                connection.Open();
                MessageBox.Show("Connecté!");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Impossible de se connecter au serveur");
                        break;
                    case 1:
                        MessageBox.Show("Identifiant ou mot de passe invalide");
                        break;
                }
            }
        }

        /// <summary>
        /// Changement du mot de passe d'un utilisateur
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nouveauPassword"></param>
        internal void UpdatePassword(int id, string nouveauPassword)
        {
            String passwordCrypte = CryptPassword(nouveauPassword);
            string query = "UPDATE utilisateur SET password= '" + passwordCrypte + "' where id_utilisateur= '" + id + "'";
            command = new MySqlCommand(query, this.connection);

            command.ExecuteNonQuery();
        }

        public List<Membre> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Récupération d'un membre par son username et password
        /// </summary>
        /// <param name="username_membre"></param>
        /// <param name="password_membre"></param>
        /// <returns>Retourne le membre en question</returns>
        public Membre GetMember(string username_membre, string password_membre)
        {
            String crypt = CryptPassword(password_membre);
            Membre membre = null;
            string query = "SELECT * FROM utilisateur where username='" + username_membre + "' and password='" + crypt + "'";
            command = new MySqlCommand(query, this.connection);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {

                reader.Read();
                membre = new Membre();
                
                
                membre.id = reader.GetInt32("id_utilisateur");
                membre.username = reader.GetString("username");
                membre.password = passwordCrypte;
                membre.firstName = reader.GetString("Prenom");
                membre.lastName = reader.GetString("Nom");
                membre.email = reader.GetString("email");
                membre.gender = (Genre) Enum.Parse(typeof(Genre),reader.GetString("sexe"));
            }
            reader.Close();

            return membre;
        }

        internal void modifierMessagePersonnel(int id, string messagePrive)
        {
            string query = "UPDATE utilisateur SET message_perso= '" + messagePrive + "' where id_utilisateur= '" + id + "'";
            command = new MySqlCommand(query, this.connection);
        }
        /// <summary>
        /// Modification des info du profil
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="email"></param>
        /// <param name="notel"></param>
        /// <param name="id"></param>
        internal void ModifierInfosAPartirProfil(string nom, string prenom, string email, string notel, int id)
        {
            string query = "UPDATE utilisateur SET nom= '" + nom + "', prenom= '" + prenom + "', email= '" + email + "'  where id_utilisateur= '" + id + "'";
            command = new MySqlCommand(query, this.connection);

            command.ExecuteNonQuery();
        }

        private string GetDBstring(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? string.Empty : Reader.GetString(SqlFieldName);
        }

        /// <summary>
        /// Vérifie si un username existe dans la base de donnée
        /// </summary>
        /// <param name="username">Username d'un membre</param>
        /// <returns>Retourne True si existant, False si non</returns>
        public bool UsernameExist(string username)
        {
            bool hasRows = false;
            string query = "SELECT * FROM utilisateur where username='"+username+"'";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            hasRows = reader.HasRows;
            reader.Close();

         return hasRows;
        }

        /// <summary>
        /// Vérifie si un adresse email existe dans la bd
        /// </summary>
        /// <param name="email">Email du membre</param>
        /// <returns>Retourne True si oui, False si non</returns>
        public bool EmailAdressExist(string email)
        {
            bool hasRows = false;
            string query = "SELECT * FROM utilisateur where email='" + email + "'";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            hasRows = reader.HasRows;
            reader.Close();

            return hasRows;
        }

        /// <summary>
        /// Récupère le password d'un membre grace à son adresse email
        /// </summary>
        /// <param name="email">Email de membre ayant oublié son password</param>
        /// <returns>Retourne le password</returns>
        public string GetPassword(string email)
        {
            string motDePasse = null;
            string query = "SELECT password FROM utilisateur where email='" + email + "'";
            command = new MySqlCommand(query, this.connection);

            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            motDePasse = reader.GetString("password");
            reader.Close();
            return motDePasse;
        }

        /// <summary>
        /// Supprime un membre de la BD grace à son id
        /// </summary>
        /// <param name="userId">Id du membre</param>
        public void DeleteMember(int userId)
        {
            string query = "DELETE FROM utilisateur where id_utilisateur='" + userId + "'";
            command = new MySqlCommand(query, this.connection);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Ajout d'un membre
        /// </summary>
        /// <param name="nouveauMembre"></param>
        /// <returns>Retourne l'id du membre inséré</returns>
        public int InsertMember(Membre nouveauMembre)
        {
            FileStream fs;
            BinaryReader br;

            string passwordCrypte = CryptPassword(nouveauMembre.password);

            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO utilisateur (username, password," +
                                  "Nom, Prenom, email, sexe, image, imgFile_name, imgFile_size) " +
                                  "VALUES (@nom_utilisateur, @mdp, @nom," +
                                  "@prenom, @email, @sexe, @image, @imgFile_name, @imgFile_size)";
            command.Parameters.AddWithValue("@nom_utilisateur", nouveauMembre.username);
            command.Parameters.AddWithValue("@mdp", passwordCrypte);
            command.Parameters.AddWithValue("@nom", nouveauMembre.lastName);
            command.Parameters.AddWithValue("@prenom", nouveauMembre.firstName);
            command.Parameters.AddWithValue("@email", nouveauMembre.email);
            command.Parameters.AddWithValue("@sexe", Enum.GetName(typeof(Genre), nouveauMembre.gender));
            command.Parameters.AddWithValue("@image", nouveauMembre.imageData);
            command.Parameters.AddWithValue("@imgFile_name", nouveauMembre.imgFileName);
            command.Parameters.AddWithValue("@imgFile_size", nouveauMembre.fileSize);

           // connection.Open();
            command.ExecuteNonQuery();
            return Convert.ToInt32(command.LastInsertedId);
        }

        /// <summary>
        /// Supprime un membre de la liste d'amis de l'utilisateur
        /// </summary>
        /// <param name="userId">Id de l'utilisateur voulant supprimer un amis</param>
        /// <param name="idAmis">Id de l'amis à supprimer</param>
        /// <returns>Retourne True si suppression réussi, False si non</returns>
        public bool DeleteFriend(int userId, int idAmis)
        {
            string query = "DELETE FROM contact WHERE id_amis='" + idAmis + "'";
            command = new MySqlCommand(query, this.connection);
            command.ExecuteNonQuery();

            return !DejaAmis(userId, idAmis);
        }

        /// <summary>
        /// Récupère la liste d'amis d'un membre
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retourne la liste d'amis d'un membre</returns>
        public ArrayList GetListAmis(int id)
        {
            ArrayList listAmis = new ArrayList();
            string query = "SELECT username FROM utilisateur where id_utilisateur IN (SELECT id_amis FROM "+
                "contact WHERE id_utilisateur='" + id + "' UNION SELECT id_utilisateur FROM contact WHERE id_amis='" + id + "')";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                 while (reader.Read())
                 {
                     listAmis.Add(reader.GetString("username"));
                 }           
            }
            reader.Close();
            return listAmis;
        }

        /// <summary>
        /// Récupère la liste de membre résultant d'un string de recherche
        /// </summary>
        /// <param name="caractereRecherche">String de recherche</param>
        /// <returns></returns>
        public ArrayList GetListResultatRecherche(string caractereRecherche)
        {
            ArrayList listResultat = new ArrayList();
            string query = "SELECT username FROM utilisateur where username LIKE '%" + caractereRecherche + "%'";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listResultat.Add(reader.GetString("username"));
                }
            }
            reader.Close();
            return listResultat;
        }

        /// <summary>
        /// Ajoute un amis dans la table contact
        /// </summary>
        /// <param name="idUtilisateur">Id du membre qui ajoute un amis</param>
        /// <param name="idAmis">Id de l'Amis ajouté dans la liste d'amis du membre</param>
        public void AjouterUnAmis(int idUtilisateur, int idAmis)
        {
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO contact (id_utilisateur, id_amis) " +
                                  "VALUES (@id_utilisateur, @id_amis)";
            command.Parameters.AddWithValue("@id_utilisateur", idUtilisateur);
            command.Parameters.AddWithValue("@id_amis", idAmis);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Récupère l'id d'un membre grace à son username
        /// </summary>
        /// <param name="username">username du membre</param>
        /// <returns>Retourne L'id du membre</returns>
        public int GetMemberIdByUsername(string username)
        {
            int idAmis = 0;
            string query = "SELECT id_utilisateur FROM utilisateur where username='" + username + "'";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                idAmis = reader.GetInt32("id_utilisateur");
            }
            reader.Close();
            return idAmis;
        }

        /// <summary>
        /// Encryption d'un motde pass
        /// </summary>
        /// <param name="passwordNonCrypte">password sans encryption</param>
        /// <returns>Retourne le password crypté</returns>
        private String CryptPassword(String passwordNonCrypte)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(passwordNonCrypte);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    passwordCrypte = Convert.ToBase64String(ms.ToArray());
                }
            }
            
            return passwordCrypte;
        }

        /// <summary>
        /// Vérifie si un membre est amis avec un autre
        /// </summary>
        /// <param name="idUtilisateur">Id du membre</param>
        /// <param name="idAmis">Id de l'autre membre</param>
        /// <returns>Retourne True si amis, False si non</returns>
        public bool DejaAmis(int idUtilisateur, int idAmis)
        {
            bool DejaAmis = false;

            string query = "SELECT id_utilisateur FROM contact where id_amis='" + idAmis + "'";
            command = new MySqlCommand(query, this.connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.GetInt32("id_utilisateur") == idUtilisateur)
                    {
                        DejaAmis = true;
                    }
                }
            }
            reader.Close();
            return DejaAmis;
        }

        public List<Membre> getAllMembers()
        {
            throw new NotImplementedException();
        }

        public void updateMember()
        {
            throw new NotImplementedException();
        }
    } 
}
    

