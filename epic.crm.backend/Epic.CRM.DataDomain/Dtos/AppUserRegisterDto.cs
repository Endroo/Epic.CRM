using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class AppUserRegisterDto : Dto<AppUser>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Profession { get; set; }

        public override AppUser Map()
        {
            return new AppUser
            {
                IsAdmin = IsAdmin,
                Email = Email,
                Name = Name,
                Profession = Profession
            };
        }

        public override AppUserRegisterDto Map(AppUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
