using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class AppUserDto : Dto<AppUser>
    {
        public int AppUserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public string Profession { get; set; }
        public string AspNetUserId { get; set; }
        public int WorkCount { get; set; } = 0;
        public int CustomerCount { get; set; } = 0;

        public override AppUser Map()
        {
            return new AppUser
            {
                AppUserId = AppUserId,
                IsAdmin = IsAdmin,
                Name = Name,
                Email = Email,
                Profession = Profession
            };
        }

        public override AppUserDto Map(AppUser entity)
        {
            return new AppUserDto
            {
                AppUserId = entity.AppUserId,
                IsAdmin = entity.IsAdmin,
                Email = entity.Email,
                Name = entity.Name,
                Profession = entity.Profession,
                WorkCount = entity.Work.Count,
                CustomerCount = entity.Customer.Count
            };
        }
    }
}
