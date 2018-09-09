#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Infra.Data.Extensions;

#endregion

namespace Shift.Infra.Data.Mappings.Cadastro
{
    public class ClasseContabilMapping : EntityTypeConfiguration<ClasseContabil>
    {
        public override void Map(EntityTypeBuilder<ClasseContabil> builder)
        {

            builder.HasKey(x => x.Codigo);



            builder.Property(x => x.Codigo)
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedNever();




            builder.Property(x => x.Nome)
                .HasColumnType("varchar(20)")
                .IsRequired();



            builder.Ignore(x => x.Id);

            builder.Ignore(x => x.IdSituacao);

            builder.Ignore(x => x.CodEmpresa);

            builder.Ignore(x => x.Excluido);

            builder.Ignore(x => x.Notifications);

            builder.Ignore(x => x.Valid);

            builder.Ignore(x => x.Invalid);

            builder.ToTable("ClasseContabeis", "Estatico");
        }
    }
}
