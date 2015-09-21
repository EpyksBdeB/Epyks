using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    public class Membre
    {
        private String name;
        private String surname;
        private String username;
        private String password;
        private String email;
        private char gender;

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

        public Membre()
        {
        }

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return name;
        }

        public void setSurname(String surname)
        {
            this.surname = surname;
        }
        public String getSurname()
        {
            return surname;
        }

        public void setUsername(String username)
        {
            this.username = username;
        }
        public String getUsername()
        {
            return username;
        }

        public void setPassword(String password)
        {
            this.password = password;
        }
        public String getPassword()
        {
            return password;
        }

        public void setEmail(String email)
        {
            this.name = name;
        }
        public String getEmail()
        {
            return name;
        }

        public void setGender(char gender)
        {
            this.gender = gender;
        }
        public char getGender()
        {
            return gender;
        }
    }
}
