﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Epic.CRM.DataDomain.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public string Name { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? WorkDateTime { get; set; }

    public string Description { get; set; }

    public int? AddressId { get; set; }

    public int? WorkStatusId { get; set; }

    public int AppUserId { get; set; }

    public int? Price { get; set; }

    public virtual Address Address { get; set; }

    public virtual AppUser AppUser { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual WorkStatus WorkStatus { get; set; }
}