using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace Epyks.Application
{
    public class MembreDTO
    {
        public String name { get; set; }
        public String surname { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String email { get; set; }
        public char gender { get; set; }

        public MembreDTO()
        {
        }
    }
}
