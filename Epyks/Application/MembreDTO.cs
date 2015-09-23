using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace Epyks.Application
{
    public class MembreDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Genre gender { get; set; }

        public MembreDTO(string firstName, string lastName, string username, string password,
            string email, Genre gender)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;
        }

        public MembreDTO()
        {
        }
    }
}
