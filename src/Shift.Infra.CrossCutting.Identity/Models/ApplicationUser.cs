using System;
using Microsoft.AspNetCore.Identity;

namespace Shift.Infra.CrossCutting.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Matricula { get; set; }
    }
}
