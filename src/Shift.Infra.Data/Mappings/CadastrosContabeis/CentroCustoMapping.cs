#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Infra.Data.Extensions;

#endregion

namespace Shift.Infra.Data.Mappings.CadastrosContabeis
{
    public class CentroCustoMapping : EntityTypeConfiguration<CentroCusto>
    {

        public override void Map(EntityTypeBuilder<CentroCusto> builder)
        {


            builder.HasKey(s => new { s.CodEmpresa, s.CodCentroCusto });

       
            //builder.HasKey(e => e.CodCentroCusto);

            builder.Property(e => e.CodCentroCusto)
                .HasColumnType("bigint")
                .IsRequired()
                .ValueGeneratedNever();



            builder.Property(e => e.NomeCentroCusto)
                .HasColumnType("varchar(200)")
                .IsRequired();



            builder.Property(e => e.OrigemLegado)
                .HasColumnType("bit")
                .IsRequired();




            //Empresas: Relacionamento do Tipo: UM para MUITOS
            
            builder.HasOne(c => c.Empresas)         //Centro de Custo possui uma Empresa
                .WithMany(e => e.CentroCustos)      //Empresa possui vários Centro de Custo
                .HasForeignKey(c => c.CodEmpresa)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);



            //Grupos: Relacionamento do Tipo: UM para MUITOS
            builder.HasOne(c        => c.Grupos)               //Centro de Custo possui um Grupo
                .WithMany(g         => g.CentroCustos)          //Grupo possui vários Centro de Custo
                .HasForeignKey(c    => c.CodGrupo)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);



            //Classes: Relacionamento do Tipo: UM para MUITOS
            builder.HasOne(c        => c.ClasseContabil)       //Centro de Custo possui uma ClasseContabil
                .WithMany(cc        => cc.CentroCustos)        //ClasseContabil possui vários Centro de Custo
                .HasForeignKey(c    => c.CodClasse)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
            


            //Tipos Bloqueios: Relacionamento do Tipo: UM para MUITOS
            builder.HasOne(c        => c.TipoBloqueios)        //Centro de Custo possui um Tipo de Bloqueio
                .WithMany(b         => b.CentroCustos)        //Tipo de Bloqueio possui vários Centro de Custo
                .HasForeignKey(c    => c.CodTipoBloqueio)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Ignore(e => e.IdSituacao);

            builder.Ignore(e => e.Id);

            builder.Ignore(e => e.Excluido);

            builder.Ignore(e => e.Notifications);

            builder.Ignore(e => e.Valid);

            builder.Ignore(e => e.Invalid);

            builder.ToTable("CentroCustos", "Cadastro");

        }
    }
}
