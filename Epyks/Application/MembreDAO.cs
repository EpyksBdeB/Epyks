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
        private string myConnectionString = null;
     

        public MembreDAO()
        {
            initializeDatabase();
        }

        public void initializeDatabase()
        {
            //myConnectionString = "server=localhost;uid=melissa_07;" + "pwd=Cartigan0;database=test;";
            myConnectionString = "server=aegaur.ddns.net;uid=root;" + "pwd=gr007,,;database=epyksdb;port=3306;";
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

        public Membre getMember()
        {
            throw new NotImplementedException();
        }

        public void updateMember()
        {
            throw new NotImplementedException();
        }

        public void deleteMember()
        {
            throw new NotImplementedException();
        }

        public void insertMember()
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
    

