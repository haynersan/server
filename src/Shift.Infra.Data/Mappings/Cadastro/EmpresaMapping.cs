#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.EmpresaModel;
using Shift.Infra.Data.Extensions;

#endregion


namespace Shift.Infra.Data.Mappings.Cadastro
{
    public class EmpresaMapping : EntityTypeConfiguration<Empresa>
    {
        public override void Map(EntityTypeBuilder<Empresa> builder)
        {


            builder.HasKey(e => e.CodEmpresa);


            builder.Property(e => e.CodEmpresa)
                .HasColumnType("varchar(04)")
                .HasMaxLength(04)
                .IsRequired()
                .ValueGeneratedNever();


            builder.Property(e => e.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();


            
            builder.OwnsOne(e => e.CNPJ, e =>
            {
                e.Property(a => a.NumeroCNPJ)
                    .HasColumnType("varchar(14)")
                    .HasColumnName("Cnpj")
                    .HasMaxLength(14)
                    .IsRequired()
                    .ValueGeneratedNever();



                e.Ignore(y => y.Notifications);

                e.Ignore(y => y.Valid);

                e.Ignore(y => y.Invalid);

            });



            //Relacionamento do Tipo: 1 para MUITOS
            builder.HasOne(e => e.Situacao) //Empresa possui uma Situacao
                .WithMany(s => s.Empresas)  //Situacao possui várias empresas
                .HasForeignKey(e => e.IdSituacao)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.Ignore(e => e.Id);

            builder.Ignore(e => e.Notifications);

            builder.Ignore(e => e.Valid);

            builder.Ignore(e => e.Invalid);

            builder.ToTable("Empresas", "Cadastro");
        }
    }
}
