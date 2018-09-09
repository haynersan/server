#region usings

using Flunt.Validations;
using Shift.Domain.Cadastro.EmpresaModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Domain.Core.Models;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel
{
    public class CentroCusto : Entity<CentroCusto>
    {

        //EF Core
        protected CentroCusto() {}


        public CentroCusto(string codEmpresa, long codCentroCusto, string nomeCentroCusto, string codGrupo, int codClasse, int codTipoBloqueio)
        {

            CodEmpresa          = codEmpresa;

            CodCentroCusto      = codCentroCusto;

            NomeCentroCusto     = nomeCentroCusto;

            CodGrupo            = codGrupo;

            CodClasse           = codClasse;

            CodTipoBloqueio     = codTipoBloqueio;


            #region ContratoDeValidacao

            AddNotifications(new Contract()

                .Requires()


                //CodEmpresa
                .IsNotNullOrEmpty(CodEmpresa, "Cod. Empresa", "O código deve ser informado")
                .HasLen(CodEmpresa, 4, "Cod. Empresa", "O código deve possuir 04 caracteres")


                //CodCentroCusto
                .IsNotNull(CodCentroCusto, "Cod. Centro Custo", "O Cód. Centro Custo deve ser informado")


                //Nome Centro Custo
                .IsNotNullOrEmpty(NomeCentroCusto, "Nome", "O nome do Centro de Custo deve ser informado")
                .HasMinLen(NomeCentroCusto, 3, "Nome", "O nome do Centro de Custo deve possuir no mínimo 3 caracteres")
                .HasMaxLen(NomeCentroCusto, 200, "Nome", "O nome do Centro de Custo deve possuir no máximo 200 caracteres")


                //CodGrupo
                .IsNotNullOrEmpty(CodGrupo, "Cod. Grupo", "O Cód. do Grupo deve ser informado")
                .HasMinLen(CodGrupo, 2, "Cod. Grupo", "O Cód. do Grupo deve possuir no mínimo 2 caracteres")
                .HasMaxLen(CodGrupo, 3, "Cod. Grupo", "O Cód. do Grupo deve possuir no máximo 3 caracteres")


                //CodClasse
                .IsNotNull(CodClasse, "Cod. Classe", "O Cód. Classe deve ser informado")


                //Cod Tipo Bloqueio
                .IsNotNull(CodTipoBloqueio, "Cod. Tipo Bloqueio", "O Cód. Tipo Bloqueio deve ser informado")

                );

            #endregion
        }


        #region Propriedades


        public long     CodCentroCusto                  { get; private set; }

        public string   NomeCentroCusto                 { get; private set; }

        public bool     OrigemLegado                    { get; private set; }

        public string   CodGrupo                        { get; private set; }

        public int      CodClasse                       { get; private set; }

        public int      CodTipoBloqueio                 { get; private set; }

        
        //Propriedades de Navegacao = EF
        public virtual Empresa          Empresas        { get; private set; }

        public virtual Grupo            Grupos          { get; private set; }

        public virtual ClasseContabil   ClasseContabil  { get; private set; }

        public virtual TipoBloqueio     TipoBloqueios   { get; private set; }

        #endregion


        #region Metodos

        public void Excluir() => Excluido = true;

        #endregion
    }
}
