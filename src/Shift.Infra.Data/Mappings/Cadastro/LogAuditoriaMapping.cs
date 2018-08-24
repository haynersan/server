#region usings

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Infra.Data.Extensions;

#endregion

namespace Shift.Infra.Data.Mappings.Cadastro
{
    public class LogAuditoriaMapping : EntityTypeConfiguration<LogAuditoria>
    {
        public override void Map(EntityTypeBuilder<LogAuditoria> builder)
        {


            builder.Property(x => x.DataOperacao)
             .HasColumnType("datetime")
             .IsRequired();



            builder.Property(x => x.Schema)
                .HasColumnType("varchar(50)")
                .IsRequired();



            builder.Property(x => x.Tabela)
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.Property(x => x.Acao)
              .HasColumnType("varchar(10)")
              .IsRequired();



            builder.Property(x => x.Modulo)
              .HasColumnType("varchar(200)")
              .IsRequired();



            builder.Property(x => x.JsonResult)
             .HasColumnType("varchar(4000)")
             .IsRequired();



            builder.Property(x => x.UserIdLogado)
                .HasColumnType("uniqueidentifier")
                .IsRequired();


            builder.Ignore(x => x.Excluido);

            builder.Ignore(x => x.IdSituacao);

            builder.Ignore(x => x.CodEmpresa);

            builder.Ignore(x => x.Notifications);

            builder.Ignore(x => x.Valid);

            builder.Ignore(x => x.Invalid);

            builder.ToTable("LogAuditorias", "Cadastro");
        }
    }
}
