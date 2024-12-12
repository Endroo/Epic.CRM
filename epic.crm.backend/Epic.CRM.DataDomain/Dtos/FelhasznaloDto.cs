using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class FelhasznaloDto : Dto<Felhasznalo>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nev { get; set; }
        public bool IsAdmin { get; set; }
        public string Tevekenyseg { get; set; }

        public override Felhasznalo Map()
        {
            return new Felhasznalo
            {
                Id = Id,
                Email = Email,
                IsAdmin = IsAdmin,
                Nev = Nev,
                Tevekenyseg = Tevekenyseg,
                UserName = Email
            };
        }

        public override FelhasznaloDto Map(Felhasznalo entity)
        {
            return new FelhasznaloDto
            {
                Id = entity.Id,
                Email = entity.Email,
                IsAdmin = entity.IsAdmin,
                Nev = entity.Nev,
                Tevekenyseg = entity.Tevekenyseg
            };
        }
    }
}
