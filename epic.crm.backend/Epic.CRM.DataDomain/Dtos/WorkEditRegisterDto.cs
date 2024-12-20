using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class WorkEditRegisterDto : Dto<Work>
    {
        public string Name { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? WorkDateTime { get; set; }

        public string Description { get; set; }

        public int? Price { get; set; }

        public int? AddressId { get; set; }

        public int? WorkStatusId { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string HouseAddress { get; set; }

        public override Work Map()
        {
            return new Work
            {
                Name = Name,
                Price = Price,
                WorkDateTime = WorkDateTime,
                Description = Description,
                AddressId = AddressId,
                WorkStatusId = WorkStatusId,
                CustomerId = CustomerId,
                Address = AddressId is null ? 
                    new Address 
                    { 
                        City = City,
                        HouseAddress = HouseAddress,
                        ZipCode = ZipCode
                    } 
                    : null
            };
        }

        public override Dto<Work> Map(Work entity)
        {
            throw new NotImplementedException();
        }

        public override Work Update(Work entity)
        {
            entity.Name = Name;
            entity.Price = Price;
            entity.Description = Description;
            entity.AddressId = AddressId;
            entity.WorkStatusId = WorkStatusId;
            entity.CustomerId = CustomerId;
            if (AddressId is null)
            {
                entity.Address =
                    new Address
                    {
                        City = City,
                        HouseAddress = HouseAddress,
                        ZipCode = ZipCode
                    };
            }
            return entity;
        }
    }
}
