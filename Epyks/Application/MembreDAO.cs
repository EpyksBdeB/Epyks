using System;
using System.Collections.Generic;
using MySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
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

        public Membre getMember(int id_membre)
        {
            throw new NotImplementedException();
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

        public void deleteMember()
        {
            throw new NotImplementedException();
        }

        // Ajouter parametre pour recevoir un membre
        public void insertMember(Membre nouveauMembre)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO utilisateurs (id_utilisateur, nom_utilisateur, mdp," +
                                  "nom, prenom, email, sexe) VALUES ('melissa07', 'melissa','Sissoko'," +
                                  " 'Christelle', 'blabla@gmail.com', 'F')";
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
    

