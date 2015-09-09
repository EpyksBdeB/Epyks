using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Presentation.Application
{
    public class Membre
    {
        private String name { get; set; }
        private String surname { get; set; }
        private String username { get; set; }
        private String password { get; set; }
        private String email { get; set; }
        private char gender { get; set; }

        public Membre(String name, String surname, String username, String password,
            String email, char gender)
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;

        }

        

    }

    

}
