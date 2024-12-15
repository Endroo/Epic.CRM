using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class AppUserEditDto : Dto<AppUser>
    {
        public bool? IsAdmin { get; set; }
        public string? Name { get; set; }
        public string? Profession { get; set; }

        public override AppUser Map()
        {
            throw new NotImplementedException();
        }

        public override Dto<AppUser> Map(AppUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
