using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    interface MembreDAOInterface
    {
        List<Membre> getAllMembers();
        Membre getMember(int id);
        int trouverUsername(String username);
        void updateMember();
        void deleteMember();
        void insertMember(Membre nouveauMembre);
    }
}
