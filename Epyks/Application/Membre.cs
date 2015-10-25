using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        internal String imgFileName { get; set; }
        internal byte[] imageData { get; set; }
        internal int fileSize { get; set; }

        internal Membre(int id,string firstName, string lastName, string username, string password,
            string email, Genre gender, String imgFileName, byte[] imageData, int fileSize)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.email = email;
            this.gender = gender;
            this.imgFileName = imgFileName;
            this.imageData = imageData;
            this.fileSize = fileSize;
        }

        internal Membre(MembreDTO membre)
        {
            this.firstName = membre.firstName;
            this.lastName = membre.lastName;
            this.username = membre.username;
            this.password = membre.password;
            this.email = membre.email;
            this.gender = membre.gender;
            this.fileSize = membre.fileSize;
            this.imgFileName = membre.imgfilename;
            this.imageData = membre.imageData;
        }

        internal Membre()
        {
        }

        internal MembreDTO getDTO()
        {
            MembreDTO mdto = new MembreDTO();
            mdto.id = this.id;
            mdto.firstName = this.firstName;
            mdto.lastName = this.lastName;
            mdto.email = this.email;
            mdto.gender = this.gender;
            mdto.password = this.password;
            mdto.username = this.username;
            mdto.fileSize = this.fileSize;
            mdto.imgfilename = this.imgFileName;
            mdto.imageData = this.imageData;

            return mdto;
        }
    }
}
