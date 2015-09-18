using System;
using System.Collections.Generic;
using MySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace Epyks.Application
{
    public class MembreDAO : MembreDAOInterface
    {
        private MySql.Data.MySqlClient.MySqlConnection connection = null;
        private MySqlCommand command;
        private string myConnectionString = null;
        private String adresseConnection = Properties.Settings.Default.Server_address;
     

        public MembreDAO()
        {
            initializeDatabase();
        }

        public void initializeDatabase()
        {
            //myConnectionString = "server=localhost;uid=melissa_07;" + "pwd=Cartigan0;database=test;";
            myConnectionString = "server="+adresseConnection+";uid=epyksbdeb;pwd=gr007,,;database=epyksbd;port=3306;";
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = myConnectionString;
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

        public List<Membre> getAllMembers()
        {
            throw new NotImplementedException();
        }

        public Membre getMember(String username_membre)
        {
            Membre unMembre = new Membre();
            String query = "SELECT * FROM utilisateur where username='"+username_membre+"'";
            command = new MySqlCommand(query, this.connection);

            MySqlDataReader reader = command.ExecuteReader();
             if (!reader.HasRows) return null;
             while (reader.Read())
             {
                Console.WriteLine(GetDBString("column1", reader));
                Console.WriteLine(GetDBString("column2", reader));
             }
            reader.Close();

            return unMembre;
        }

        private string GetDBString(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? String.Empty : Reader.GetString(SqlFieldName);
        }

        public int trouverUsername(string username)
        {
            String query = "SELECT COUNT(*) FROM utilisateur where username='"+username+"'";
            command = new MySqlCommand(query, this.connection);

                 object rows = command.ExecuteScalar();
                 if ((rows == null) || (rows == DBNull.Value)) return -1;

         return Convert.ToInt32(rows);
        }

        public void updateMember()
        {
            throw new NotImplementedException();
        }

        public void deleteMember(String username)
        {
            String query = "DELETE FROM utilisateur where username='"+username+"'";
            command = new MySqlCommand(query, this.connection);
            command.ExecuteNonQuery();
        }

        // Ajouter parametre pour recevoir un membre
        public void insertMember(Membre nouveauMembre)
        {
            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO utilisateur (username, password," +
                                  "Nom, Prenom, email) VALUES (@nom_utilisateur, @mdp, @nom," +
                                  "@prenom, @email)";
            command.Parameters.AddWithValue("@nom_utilisateur", nouveauMembre.getUsername());
            command.Parameters.AddWithValue("@mdp", nouveauMembre.getPassword());
            command.Parameters.AddWithValue("@nom", nouveauMembre.getSurname());
            command.Parameters.AddWithValue("@prenom", nouveauMembre.getName());
            command.Parameters.AddWithValue("@email", nouveauMembre.getEmail());
            //command.Parameters.AddWithValue("@sexe", 'F');
           // connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
    

