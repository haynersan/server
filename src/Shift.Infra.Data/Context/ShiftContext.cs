#region usings

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Infra.Data.Extensions;
using Shift.Infra.Data.Mappings.Cadastro;

#endregion


namespace Shift.Infra.Data.Context
{
    public class ShiftContext : DbContext
    {

        #region Models.Domain.Cadastro
        //Necessidade do EF. Ele só irá mapear classes declaradas no DbSet<T>

        public DbSet<TipoBloqueio>  TipoBloqueio    { get; set; }

        public DbSet<Situacao>      Situacao        { get; set; }


        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Mapping.Cadastro
            //Aqui estamos focados em modificar como as tabelas serão refletidas no banco.

            modelBuilder.AddConfiguration(new TipoBloqueioMapping());

            modelBuilder.AddConfiguration(new SituacaoMapping());

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
