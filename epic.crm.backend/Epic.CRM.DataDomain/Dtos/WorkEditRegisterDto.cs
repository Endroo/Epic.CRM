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

        public AddressDto Address { get; set; }

        public int? WorkStatusId { get; set; }

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
                Address = AddressId is not null ? Address.Map() : null
            };
        }

        public override Dto<Work> Map(Work entity)
        {
            throw new NotImplementedException();
        }
    }
}
