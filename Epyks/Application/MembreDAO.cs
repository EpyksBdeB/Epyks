using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Epyks.Application
{
    public class MembreDAO : MembreDAOInterface
    {
        // Probleme avec l'implementation de la BD donc pour le moment je travaille avec une liste
        List<Membre> listofMembers;

        public MembreDAO()
        {
            listofMembers = new List<Membre>();
            Membre member1 = new Membre("Robert", "Gallagher", "Gally", "root", "gally@hotmail.fr", 'M');
            Membre member2 = new Membre("Robert", "Gallagher", "Gally", "root", "gally@hotmail.fr", 'M');
            listofMembers.Add(member1);
            listofMembers.Add(member2);	
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
    

