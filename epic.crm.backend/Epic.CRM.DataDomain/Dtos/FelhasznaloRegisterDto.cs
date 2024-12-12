using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class FelhasznaloRegisterDto : Dto<Felhasznalo>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nev { get; set; }
        public bool IsAdmin { get; set; }
        public string Tevekenyseg { get; set; }

        public override Felhasznalo Map()
        {
            return new Felhasznalo
            {
                Email = Email,
                IsAdmin = IsAdmin,
                Nev = Nev,
                Tevekenyseg = Tevekenyseg,
                UserName = Email
            };
        }

        public override FelhasznaloRegisterDto Map(Felhasznalo entity)
        {
            throw new NotImplementedException();
        }
    }
}
