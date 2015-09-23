using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    public enum Genre : int
    {
        MALE,
        FEMALE
    }

    internal class Membre
    {

        internal int id { get; set; }
        internal string firstName { get; set; }
        internal string lastName { get; set; }
        internal string username { get; set; }
        internal string password { get; set; }
        internal string email { get; set; }
        internal Genre gender { get; set; }

        internal Membre(int id,string firstName, string lastName, string username, string password,
            string email, Genre gender)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;
        }

        internal Membre(MembreDTO membre)
        {
            this.firstName = membre.firstName;
            this.lastName = membre.lastName;
            this.username = membre.username;
            this.password = membre.password;
            this.email = membre.email;
            this.gender = membre.gender;
        }

        internal Membre()
        {
        }
    }
}
