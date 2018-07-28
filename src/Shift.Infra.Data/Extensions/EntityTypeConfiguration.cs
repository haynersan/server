using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Shift.Infra.Data.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}


/*------------------------------------------------------------------------------------------------------------------
 
    Por meio desta Classe é possível criar nossos Mappings fora da classe de contexto

-------------------------------------------------------------------------------------------------------------------*/
