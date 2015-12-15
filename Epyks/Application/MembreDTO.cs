using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using Epyks.Coordonnateur;

namespace Epyks.Application
{
    public class MembreDTO
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public Genre gender { get; set; }
        public String imgfilename { get; set; }
        public byte[] imageData { get; set; }
        public int fileSize { get; set; }


        public MembreDTO(string firstName, string lastName, string username, string password,
            string email, Genre gender, String imgfilename, byte[] imageData, int fileSize)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;
            this.imgfilename = imgfilename;
            this.imageData = imageData;
            this.fileSize = fileSize;
        }

        public MembreDTO()
        {
        }
    }
}
