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
        Membre getMember(string username, string password);
        bool UsernameExist(string username);
        void updateMember();
        void deleteMember(int userId);
        int insertMember(Membre nouveauMembre);
    }
}
