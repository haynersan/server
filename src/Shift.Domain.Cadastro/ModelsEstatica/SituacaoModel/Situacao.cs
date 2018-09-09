#region usings

using System;
using System.Collections.Generic;
using Shift.Domain.Cadastro.EmpresaModel;
using Shift.Domain.Core.Models;

#endregion


namespace Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel
{
    public class Situacao : Entity<Situacao>
    {

        /// <Documentacao>
        /// 
        /// Esta Classe será mantida de forma MANUAL. Os dados armazeados na tabela
        /// serão estáticos e deverão obrigatoriamente a coindidir o Enum "ESituacao"
        /// 
        /// </Documentacao>


        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected Situacao() { }


        #region Propriedades

        public string   DescSituacao { get; private set; }


        // EF Propriedade de Navegação. Relação é do tipo UM para MUITOS.
        public virtual ICollection<Empresa> Empresas { get; set; }

        #endregion

    }
}
