using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class CustomerDto : Dto<Customer>
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int? AddressId { get; set; }

        public string AddressLiteral { get; set; }

        public AddressDto Address { get; set; }

        public int AppUserId { get; set; }

        public override Customer Map()
        {
            return new Customer
            { 
                Name = Name,
                Email = Email,
                AddressId = AddressId,
                AppUserId = AppUserId
            };
        }

        public override CustomerDto Map(Customer entity)
        {
            var address = entity.Address;
            var addressDto = new AddressDto().Map(address);

            return new CustomerDto
            { 
                CustomerId = entity.CustomerId,
                Name = entity.Name,
                Email = entity.Email,
                AddressId = entity.AddressId,
                AppUserId = entity.AppUserId,
                Address = addressDto,
                AddressLiteral = addressDto.ToString()
            };
        }

    }
}
