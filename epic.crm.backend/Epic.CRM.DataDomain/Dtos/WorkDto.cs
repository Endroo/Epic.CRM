using Epic.CRM.DataDomain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epic.CRM.DataDomain.Dtos
{
    public class WorkDto : Dto<Work>
    {
        public int WorkId { get; set; }

        public string Name { get; set; }

        public int? CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime? WorkDateTime { get; set; }

        public string Description { get; set; }

        public int? AddressId { get; set; }

        public string AddressLiteral { get; set; }

        public int? WorkStatusId { get; set; }

        public string WorkStatus { get; set; }

        public int AppUserId { get; set; }

        public int? Price { get; set; }

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
                AppUserId = AppUserId,
                CustomerId = CustomerId
            };
        }

        public override WorkDto Map(Work entity)
        {
            var address = entity.Address;
            return new WorkDto
            {
                WorkId = entity.WorkId,
                Name = entity.Name,
                Price = entity.Price,
                Description = entity.Description,
                WorkDateTime = entity.WorkDateTime,
                AddressId = address.AddressId,
                AppUserId = entity.AppUserId,
                CustomerId = entity.CustomerId,
                WorkStatusId = entity.WorkStatusId,
                AddressLiteral = string.Join(" ", address.ZipCode, address.City, address.HouseAddress),
                CustomerName = entity.Customer.Name,
                WorkStatus = entity.WorkStatus.Name
            };
        }

        
    }
}
