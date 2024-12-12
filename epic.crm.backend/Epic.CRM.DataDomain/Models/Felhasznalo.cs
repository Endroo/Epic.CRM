using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;

namespace Epic.CRM.DataDomain.Models;

public partial class Felhasznalo : IdentityUser
{
    public string Nev { get; set; }

    public string Tevekenyseg { get; set; }

    public bool IsAdmin { get; set; }
}