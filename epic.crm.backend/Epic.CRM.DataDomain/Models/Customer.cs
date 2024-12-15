﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Epic.CRM.DataDomain.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public int? AddressId { get; set; }

    public int AppUserId { get; set; }

    public virtual Address Address { get; set; }

    public virtual AppUser AppUser { get; set; }

    public virtual ICollection<Work> Work { get; set; } = new List<Work>();
}