#region usings

using System.Collections.Generic;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel
{
    public class ClasseContabil : Entity<ClasseContabil>
    {

        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL. 
        /// Os dados desta tabela coincidirá com os tipo de Classe Contábil do Protheus
        /// 
        /// </Documentacao>

        //EF Core
        protected ClasseContabil() {}

        #region Propriedades

        public int      Codigo  { get; private set; }

        public string   Nome    { get; private set; }

        // EF Propriedade de Navegação. Relação é do tipo UM para MUITOS.
        public virtual ICollection<CentroCusto> CentroCustos { get; set; }

        #endregion
    }
}
