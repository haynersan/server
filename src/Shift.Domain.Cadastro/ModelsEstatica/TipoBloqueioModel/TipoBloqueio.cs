using Shift.Domain.Core.Models;

namespace Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel
{
    public class TipoBloqueio : Entity<TipoBloqueio>
    {


        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL. Os dados armazeados na tabela
        /// Os dados desta tabela coincidirá com os tipo de Bloqueio do Protheus
        /// 
        /// </Documentacao>


        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected TipoBloqueio() { }


        #region Propriedades

        public int      Codigo  { get; private set; }

        public string   Tipo    { get; private set; }

        #endregion
    }
}
