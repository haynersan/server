#region usings

using System.Collections.Generic;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel
{
    public class TipoBloqueio : Entity<TipoBloqueio>
    {


        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL. 
        /// Os dados desta tabela coincidirá com os tipo de Bloqueio do Protheus
        /// 
        /// </Documentacao>


        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected TipoBloqueio() { }


        #region Propriedades

        public int      Codigo  { get; private set; }

        public string   Tipo    { get; private set; }

        // EF Propriedade de Navegação. Relação é do tipo UM para MUITOS.
        public virtual ICollection<CentroCusto> CentroCustos { get; set; }

        #endregion
    }
}
