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
        Membre GetMember(string username, string password);
        bool UsernameExist(string username);
        void updateMember();
        void DeleteMember(int userId);
        int InsertMember(Membre nouveauMembre);
    }
}
