using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class FelhasznaloEditDto
    {
        public bool IsAdmin { get; set; }
        public string Nev { get; set; }
        public string Tevekenyseg { get; set; }
    }
}
