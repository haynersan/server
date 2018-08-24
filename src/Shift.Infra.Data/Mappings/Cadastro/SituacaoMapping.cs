#region usings

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Infra.Data.Extensions;

#endregion

namespace Shift.Infra.Data.Mappings.Cadastro
{
    public class SituacaoMapping : EntityTypeConfiguration<Situacao>
    {

        public override void Map(EntityTypeBuilder<Situacao> builder)
        {


            builder.HasKey(x => x.IdSituacao);



            builder.Property(x => x.IdSituacao)
                .ValueGeneratedNever();




            builder.Property(x => x.DescSituacao)
                .HasColumnType("varchar(20)")
                .IsRequired();


            builder.Property(x => x.DataCadastro)
                .HasColumnType("date")
                .HasDefaultValue(DateTime.Today)
                .IsRequired();



            builder.Ignore(x => x.Id);

            builder.Ignore(x => x.CodEmpresa);

            builder.Ignore(x => x.Notifications);

            builder.Ignore(x => x.Valid);

            builder.Ignore(x => x.Invalid);

            builder.ToTable("Situacoes", "Estatico");
        }
    }
}
