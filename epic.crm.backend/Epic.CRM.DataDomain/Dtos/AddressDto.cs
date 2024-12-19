using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class AddressDto : Dto<Address>
    {
        public int AddressId { get; set; }
        public int? ZipCode { get; set; }
        public string City { get; set; }
        public string HouseAddress { get; set; }

        public override Address Map()
        {
            return new Address
            {
                ZipCode = ZipCode,
                City = City,
                HouseAddress = HouseAddress
            };
        }

        public override AddressDto Map(Address entity)
        {
            return new AddressDto
            {
                ZipCode = entity.ZipCode,
                City = entity.City,
                HouseAddress = entity.HouseAddress,
                AddressId = entity.AddressId
            };
        }
    }
}
