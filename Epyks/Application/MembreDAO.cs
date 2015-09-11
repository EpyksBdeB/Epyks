using System;
using System.Collections.Generic;
using MySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;

namespace Epyks.Application
{
    public class MembreDAO : MembreDAOInterface
    {
        // Probleme avec l'implementation de la BD donc pour le moment je travaille avec une liste
       // List<Membre> listofMembers;
        private MySql.Data.MySqlClient.MySqlConnection connection = null;
        private string myConnectionString = null;
     

        public MembreDAO()
        {
            initializeDatabase();
            /** listofMembers = new List<Membre>();
            Membre member1 = new Membre("Robert", "Gallagher", "Gally", "root", "gally@hotmail.fr", 'M');
            Membre member2 = new Membre("Robert", "Gallagher", "Gally", "root", "gally@hotmail.fr", 'M');
            listofMembers.Add(member1);
            listofMembers.Add(member2);	 **/
        }

        public void initializeDatabase()
        {
            myConnectionString = "server=localhost;uid=melissa_07;" + "pwd=Cartigan0;database=test;";
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                // message erreur
                MessageBox.Show(ex.Message);

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
    }
}
    

