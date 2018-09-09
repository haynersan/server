#region usings

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Domain.Cadastro.EmpresaModel;
using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClaimModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Infra.Data.Extensions;
using Shift.Infra.Data.Mappings.Cadastro;
using Shift.Infra.Data.Mappings.CadastrosContabeis;

#endregion


namespace Shift.Infra.Data.Context
{
    public class ShiftContext : DbContext
    {

        
        #region Models.Domain.Cadastro
        //Necessidade do EF. Ele só irá mapear classes declaradas no DbSet<T>

        public DbSet<ClaimValue>    ClaimValues         { get; set; }

        public DbSet<ClasseContabil> ClasseContabeis    { get; set; }

        public DbSet<Empresa>       Empresas            { get; set; }

        public DbSet<Grupo>         Grupos              { get; set; }

        public DbSet<LogAuditoria>  LogAuditorias       { get; set; }

        public DbSet<Situacao>      Situacoes           { get; set; }

        public DbSet<TipoBloqueio>  TipoBloqueios       { get; set; }


        #endregion

        
        #region Models.Domain.Cadastro.Contabeis

        public DbSet<CentroCusto> CentroCustos { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Mapping.Cadastro
            //Aqui estamos focados em modificar como as tabelas serão refletidas no banco.

            modelBuilder.AddConfiguration(new ClaimValueMapping());

            modelBuilder.AddConfiguration(new ClasseContabilMapping());

            modelBuilder.AddConfiguration(new EmpresaMapping());

            modelBuilder.AddConfiguration(new GrupoMapping());

            modelBuilder.AddConfiguration(new LogAuditoriaMapping());

            modelBuilder.AddConfiguration(new SituacaoMapping());

            modelBuilder.AddConfiguration(new TipoBloqueioMapping());

            #endregion


            #region Mapping.CadastroContabeis

            modelBuilder.AddConfiguration(new CentroCustoMapping());

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
