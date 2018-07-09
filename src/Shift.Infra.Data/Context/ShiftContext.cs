#region usings

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#endregion


namespace Shift.Infra.Data.Context
{
    public class ShiftContext : DbContext
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Mapping.Cadastro
            //Aqui estamos focados em modificar como as tabelas serão refletidas no banco.


            #endregion

            base.OnModelCreating(modelBuilder);
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

    }
}
