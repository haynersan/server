#region usings

using System.Collections.Generic;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.GrupoModel
{
    public class Grupo : Entity<Grupo>
    {

        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL. 
        /// 
        /// </Documentacao>


        //EF Core
        protected Grupo() {}


        #region Propriedades

        public string Codigo    { get; private set; }

        public string Nome      { get; private set; }

        // EF Propriedade de Navegação. Relação é do tipo UM para MUITOS.
        public virtual ICollection<CentroCusto> CentroCustos { get; set; }

        #endregion
    }
}
