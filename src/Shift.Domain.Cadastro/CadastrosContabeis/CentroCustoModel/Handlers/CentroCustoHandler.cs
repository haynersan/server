#region usings

using System;
using System.Linq;
using Flunt.Notifications;
using Newtonsoft.Json;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Commands.Inputs;
using Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Repository;
using Shift.Domain.Cadastro.EmpresaModel.Repository;
using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Domain.Cadastro.ModelsEstatica.ClasseContabilModel;
using Shift.Domain.Cadastro.ModelsEstatica.GrupoModel;
using Shift.Domain.Cadastro.ModelsEstatica.TipoBloqueioModel;
using Shift.Domain.Core.Enums;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;

#endregion

namespace Shift.Domain.Cadastro.CadastrosContabeis.CentroCustoModel.Handlers
{
    public class CentroCustoHandler :
        Notifiable,
        IHandler<AdicionarCentroCustoCommand>,
        IHandler<EditarCentroCustoCommand>,
        IHandler<ExcluirCentroCustoCommand>
    {

        protected Guid UserIdLogado;

        private readonly ICentroCustoRepository     _centroCustoRepository;

        private readonly IEmpresaRepository         _empresaRepository;

        private readonly IGrupoRepository           _grupoRepository;

        private readonly IClasseContabilRepository  _classeContabilRepository;

        private readonly ITipoBloqueioRepository    _tipoBloqueioRepository;

        private readonly ILogAuditoriaRepository    _logAuditoriaRepository;


        public CentroCustoHandler(  IUser user, ICentroCustoRepository centroCustoRepository, IEmpresaRepository empresaRepository, IGrupoRepository grupoRepository,
                                    IClasseContabilRepository classeContabilRepository, ITipoBloqueioRepository tipoBloqueioRepository, ILogAuditoriaRepository logAuditoriaRepository)
        {


            if (user.IsAuthenticated())
            {
                UserIdLogado = user.GetUserId();
            }


            _centroCustoRepository      = centroCustoRepository;

            _empresaRepository          = empresaRepository;

            _grupoRepository            = grupoRepository;

            _classeContabilRepository   = classeContabilRepository;

            _tipoBloqueioRepository     = tipoBloqueioRepository;

            _logAuditoriaRepository     = logAuditoriaRepository;

        }



        public ICommandResult Handle(AdicionarCentroCustoCommand command)
        {

            // Fail Fast Validations
            command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }


            
            //Validar Empresa
            var empresa = _empresaRepository.Buscar(e => e.CodEmpresa == command.CodEmpresa && e.IdSituacao == (int)ESituacao.Ativo).FirstOrDefault();
            if (empresa == null)
            {

                AddNotification("Cod Empresa", "O Código de Empresa informado não existe ou Empresa não está ativa");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }

                


            
            //Validar Grupo
            var grupo = _grupoRepository.Buscar(g => g.Codigo == command.CodGrupo).FirstOrDefault();
            if (grupo == null)
            {

                AddNotification("Cod Grupo", "O Código de Grupo informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }
                



            //Validar Classe
            var classe = _classeContabilRepository.Buscar(c => c.Codigo == command.CodClasse).FirstOrDefault();
            if (classe == null)
            {

                AddNotification("Cod Classe", "O Código de Classe informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }
                



            //Validar TipoBloqueio
            var tipoBloqueio = _tipoBloqueioRepository.Buscar(t => t.Codigo == command.CodTipoBloqueio).FirstOrDefault();
            if (tipoBloqueio == null)
            {

                AddNotification("Tipo Bloqueio", "O Tipo de Bloqueio informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }
                



            //Validar Centro de Custo
            var centroCusto = _centroCustoRepository.Buscar(c => c.CodEmpresa == command.CodEmpresa && c.CodCentroCusto    == command.CodCentroCusto).FirstOrDefault();
            if (centroCusto != null)
            {

                AddNotification("Duplicidade", "O registro já esta cadastrado na base");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }
                



            //Gerar as Entidades
            var centroCustoModel = new CentroCusto(command.CodEmpresa, command.CodCentroCusto, command.NomeCentroCusto, command.CodGrupo, command.CodClasse, command.CodTipoBloqueio);


            //Auditoria
            var auditoria = new LogAuditoria("Cadastro", centroCustoModel.GetType().Name, EAcao.Adicionar, centroCustoModel.GetType().Namespace, JsonConvert.SerializeObject(command), UserIdLogado);



            // Relacionamentos (Se houver)



            // Agrupar as Validações
            AddNotifications(centroCustoModel);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _centroCustoRepository.Adicionar(centroCustoModel);



            _logAuditoriaRepository.Adicionar(auditoria);



            // Enviar E-mail de boas vindas (Se houver necessidade)



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");


        }



        public ICommandResult Handle(EditarCentroCustoCommand command)
        {

            //Validar Empresa
            var empresa = _empresaRepository.Buscar(e => e.CodEmpresa == command.CodEmpresa && e.IdSituacao == (int)ESituacao.Ativo).FirstOrDefault();
            if (empresa == null)
            {

                AddNotification("Cod Empresa", "O Código de Empresa informado não existe ou Empresa não está ativa");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }





            //Validar Grupo
            var grupo = _grupoRepository.Buscar(g => g.Codigo == command.CodGrupo).FirstOrDefault();
            if (grupo == null)
            {

                AddNotification("Cod Grupo", "O Código de Grupo informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }




            //Validar Classe
            var classe = _classeContabilRepository.Buscar(c => c.Codigo == command.CodClasse).FirstOrDefault();
            if (classe == null)
            {

                AddNotification("Cod Classe", "O Código de Classe informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }




            //Validar TipoBloqueio
            var tipoBloqueio = _tipoBloqueioRepository.Buscar(t => t.Codigo == command.CodTipoBloqueio).FirstOrDefault();
            if (tipoBloqueio == null)
            {

                AddNotification("Tipo Bloqueio", "O Tipo de Bloqueio informado não existe");

                return new CommandResult(false, "Não foi possível adicionar o registro");
            }



            //Validar Centro de Custo
            var centroCusto = _centroCustoRepository.Buscar(c => c.CodEmpresa == command.CodEmpresa && c.CodCentroCusto == command.CodCentroCusto).FirstOrDefault();
            if (centroCusto == null)
            {

                AddNotification("Inexistente", "O registro informado não existe na APP");

                return new CommandResult(false, "Não foi possível editar o registro");
            }


            if (centroCusto.OrigemLegado == true)
            {

                AddNotification("Legado", "Não é permitido a edição deste registro. Pertence a um sistema legado");

                return new CommandResult(false, "Não é permitido a edição deste registro. Pertence a um sistema legado");

            }



            //Gerar as Entidades
            var centroCustoModel = new CentroCusto(command.CodEmpresa, command.CodCentroCusto, command.NomeCentroCusto, command.CodGrupo, command.CodClasse, command.CodTipoBloqueio);


            //Auditoria
            var auditoria = new LogAuditoria("Cadastro", centroCustoModel.GetType().Name, EAcao.Atualizar, centroCustoModel.GetType().Namespace, JsonConvert.SerializeObject(command), UserIdLogado);



            // Relacionamentos (Se houver)



            // Agrupar as Validações
            AddNotifications(centroCustoModel);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _centroCustoRepository.Atualizar(centroCustoModel);



            _logAuditoriaRepository.Adicionar(auditoria);



            // Enviar E-mail de boas vindas (Se houver necessidade)



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");



        }



        public ICommandResult Handle(ExcluirCentroCustoCommand command)
        {


            //Verificar se o Centro de Custo existe
            var registro = _centroCustoRepository.Buscar(c => c.CodEmpresa == command.CodEmpresa && c.CodCentroCusto == command.CodCentroCusto).FirstOrDefault();



            if (registro == null)
                return new CommandResult(false, "Não foi possível realizar esta operação. O registro informado não existe");



            if (registro.OrigemLegado == true)
            {
                return new CommandResult(false, "Não é permitido a exclusão deste registro. Pertence a um sistema legado");
            }



            //Auditoria
            //var auditoria = new LogAuditoria("Cadastro", registro.GetType().Name, EAcao.Excluir, registro.GetType().Namespace, JsonConvert.SerializeObject(registro));




            // Agrupar as Validações
            AddNotifications(registro);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _centroCustoRepository.Excluir(command.CodEmpresa, command.CodCentroCusto);



            //_auditoriaRepository.Adicionar(auditoria);



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");

        }
    }
}
