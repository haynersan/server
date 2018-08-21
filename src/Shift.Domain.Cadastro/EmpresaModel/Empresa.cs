#region usings

using Flunt.Validations;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Core.Models;
using Shift.Domain.Core.ValueObjects;

#endregion


namespace Shift.Domain.Cadastro.EmpresaModel
{
    public class Empresa : Entity<Empresa>
    {

        //Construtor Necessário para atender a uma demanda do Entity Framework
        protected Empresa() { }



        public Empresa(string codEmpresa, string nome, CNPJ cnpj, int idSituacao)
        {

            CodEmpresa  = codEmpresa;

            Nome        = nome;

            CNPJ        = cnpj;

            IdSituacao  = idSituacao;



            #region ContratoDeValidacao

            AddNotifications(new Contract()

                .Requires()


                //CodEmpresa
                .IsNotNullOrEmpty(CodEmpresa, "Cod. Empresa", "O código deve ser informado")
                .HasLen(CodEmpresa, 4, "Cod. Empresa", "O código deve possuir 04 caracteres")


                //Nome
                .IsNotNullOrEmpty(Nome, "Nome", "O nome deve ser informado")
                .HasMinLen(Nome, 3, "Nome", "O nome deve possuir no mínimo 3 caracteres")
                .HasMaxLen(Nome, 50, "Nome", "O nome deve possuir no máximo 50 caracteres")

                );

            #endregion

        }



        #region Propriedades

        public string   Nome                { get; private set; }

        public CNPJ     CNPJ                { get; private set; }

        public virtual Situacao Situacao    { get; private set; }

        #endregion



        #region Metodos

        public void Excluir() => Excluido = true;

        #endregion
    }
}
