using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epyks.Application
{
    public class MembreDTO
    {
        private String name { get; set; }
        private String surname { get; set; }
        private String username { get; set; }
        private String password { get; set; }
        private String email { get; set; }
        private char gender { get; set; }

        public MembreDTO()
        {
        }
    }
}
