using Shift.Domain.Core.Models;

namespace Shift.Domain.Cadastro.ModelsEstatica.ClaimModel
{
    public class ClaimValue : Entity<ClaimValue>
    {


        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL.
        ///  
        /// </Documentacao>


        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected ClaimValue()
        {

        }


        #region Propriedades

        public int      Codigo   { get; private set; }

        public string   Valor    { get; private set; }

        #endregion
    }
}
