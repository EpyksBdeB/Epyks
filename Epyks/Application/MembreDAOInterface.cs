using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epyks.Application.Membre;

namespace Epyks.Application
{
    interface MembreDAOInterface
    {
        public List<Membre> getAllMembers();
        public Membre getMember();
        public Membre updateMember();
        public Membre deleteMember();
    }
}
