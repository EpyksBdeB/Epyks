
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

        internal void updatePassword(int id, string nouveauPassword)
        {
            String passwordCrypte = cryptPassword(nouveauPassword);
            string query = "UPDATE utilisateur SET password= '" + passwordCrypte + "' where id_utilisateur= '" + id + "'";
            command = new MySqlCommand(query, this.connection);

            command.ExecuteNonQuery();
        }

        public List<Membre> getAllMembers()
        {
            throw new NotImplementedException();
        }

        public Membre getMember(string username_membre, string password_membre)
        {
            String crypt = cryptPassword(password_membre);
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

        internal void modifierInfosAPartirProfil(string nom, string prenom, string email, string notel, int id)
        {
            string query = "UPDATE utilisateur SET nom= '" + nom + "', prenom= '" + prenom + "', email= '" + email + "'  where id_utilisateur= '" + id + "'";
            command = new MySqlCommand(query, this.connection);

            command.ExecuteNonQuery();
        }

        private string GetDBstring(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? string.Empty : Reader.GetString(SqlFieldName);
        }

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

        public string getPassword(string email)
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

        public void updateMember()
        {
            throw new NotImplementedException();
        }

        public void deleteMember(int userId)
        {
            string query = "DELETE FROM utilisateur where id_utilisateur='" + userId + "'";
            command = new MySqlCommand(query, this.connection);
            command.ExecuteNonQuery();
        }

        // Ajouter parametre pour recevoir un membre
        public int insertMember(Membre nouveauMembre)
        {
            FileStream fs;
            BinaryReader br;

            string passwordCrypte = cryptPassword(nouveauMembre.password);

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

        //TABLE CONTACT A FAIRE
        //    contient:
        //                id_utilisateur
        //                id_amis


        public bool deleteFriend(int userId, int idAmis)
        {
            string query = "DELETE FROM contact WHERE id_amis='" + idAmis + "'";
            command = new MySqlCommand(query, this.connection);
            command.ExecuteNonQuery();

            return !dejaAmis(userId, idAmis);
        }

        public ArrayList getListAmis(int id)
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

        public ArrayList getListResultatRecherche(string caractereRecherche)
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

        public void ajouterUnAmis(int idUtilisateur, int idAmis)
        {
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO contact (id_utilisateur, id_amis) " +
                                  "VALUES (@id_utilisateur, @id_amis)";
            command.Parameters.AddWithValue("@id_utilisateur", idUtilisateur);
            command.Parameters.AddWithValue("@id_amis", idAmis);
            command.ExecuteNonQuery();
        }

        public int getMemberIdByUsername(string username)
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

        private String cryptPassword(String passwordNonCrypte)
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

        public bool dejaAmis(int idUtilisateur, int idAmis)
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


      /*  private String decryptPassword(String passwordCrypte)
        {
            String passwordDecrypte = null;
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(passwordCrypte);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    passwordDecrypte = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return passwordDecrypte;
        }*/
    } 
}
    

