using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shift.Infra.CrossCutting.Identity.Models;

namespace Shift.Infra.CrossCutting.Identity.Context
{
    public class IdentityContext : IdentityDbContext<Usuario, UsuarioRole, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
    }
}
