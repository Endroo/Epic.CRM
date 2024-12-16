using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class CustomerRegisterDto : Dto<Customer>
    {
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string HouseAddress { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public override Customer Map()
        {
            return new Customer
            {
                Name = Name,
                Email = Email,
                Address = new Address
                {
                    ZipCode = ZipCode,
                    City = City,
                    HouseAddress = HouseAddress
                }
            };
        }

        public override Dto<Customer> Map(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
