#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Infra.Data.Extensions;

#endregion

namespace Shift.Infra.Data.Mappings.Cadastro
{
    public class GrupoMapping : EntityTypeConfiguration<Grupo>
    {
        public override void Map(EntityTypeBuilder<Grupo> builder)
        {

            builder.HasKey(x => x.Codigo);



            builder.Property(x => x.Codigo)
                .HasColumnType("varchar(03)")
                .IsRequired()
                .ValueGeneratedNever();




            builder.Property(x => x.Nome)
                .HasColumnType("varchar(50)")
                .IsRequired();



            builder.Ignore(x => x.Id);

            builder.Ignore(x => x.IdSituacao);

            builder.Ignore(x => x.CodEmpresa);

            builder.Ignore(x => x.Excluido);

            builder.Ignore(x => x.Notifications);

            builder.Ignore(x => x.Valid);

            builder.Ignore(x => x.Invalid);

            builder.ToTable("Grupos", "Estatico");
        }
    }
}
