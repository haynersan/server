﻿#region usings

using System;
using System.Linq;
using Flunt.Notifications;
using Newtonsoft.Json;
using Shift.Domain.Cadastro.EmpresaModel.Commands.Inputs;
using Shift.Domain.Cadastro.EmpresaModel.Repository;
using Shift.Domain.Cadastro.LogAuditoriaModel;
using Shift.Domain.Cadastro.ModelsEstatica.SituacaoModel;
using Shift.Domain.Core.Enums;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;
using Shift.Domain.Core.ValueObjects;

#endregion


namespace Shift.Domain.Cadastro.EmpresaModel.Handlers
{
    public class EmpresaHandler :
        Notifiable,
        IHandler<AdicionarEmpresaCommand>,
        IHandler<AtualizarEmpresaCommand>,
        IHandler<ExcluirEmpresaCommand>

    {

        protected Guid UserIdLogado;

        private readonly IEmpresaRepository         _empresaRepository;

        private readonly ISituacaoRepository        _situacaoRepository;

        private readonly ILogAuditoriaRepository    _logAuditoriaRepository;


        public EmpresaHandler(IUser user, IEmpresaRepository empresaRepository, ISituacaoRepository situacaoRepository, ILogAuditoriaRepository logAuditoriaRepository)
        {


            if (user.IsAuthenticated())
            {
                UserIdLogado = user.GetUserId();
            }

            _empresaRepository          = empresaRepository;


            _situacaoRepository         = situacaoRepository;


            _logAuditoriaRepository     = logAuditoriaRepository;
        }



        public ICommandResult Handle(AdicionarEmpresaCommand command)
        {

            // Fail Fast Validations
            command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }


            //Verificar se o Codigo ou Nome informado já está em Uso
            if (_empresaRepository.ChecarSeEmpresaExiste((int)EAcao.Adicionar, command.CodEmpresa, command.Nome, command.Cnpj))
                AddNotification("Código/Nome/CNPJ", "O Código, Nome ou CNPJ já estão em uso");


            //Gerar os VOs (Se houver)
            var cnpj = new CNPJ(command.Cnpj);

            if (cnpj.Invalid)
            {
                AddNotifications(cnpj);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }

            //Gerar as Entidades
            var empresa = new Empresa(command.CodEmpresa, command.Nome, cnpj, command.IdSituacao);


            //Auditoria
            var auditoria = new LogAuditoria("Cadastro", empresa.GetType().Name, EAcao.Adicionar, empresa.GetType().Namespace, JsonConvert.SerializeObject(command), UserIdLogado );



            // Relacionamentos (Se houver)



            // Agrupar as Validações
            AddNotifications(empresa);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _empresaRepository.Adicionar(empresa);



            _logAuditoriaRepository.Adicionar(auditoria);



            // Enviar E-mail de boas vindas (Se houver necessidade)



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");

        }



        public ICommandResult Handle(AtualizarEmpresaCommand command)
        {
            
            // Fail Fast Validations
            command.Validar();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }


            var situacao = _situacaoRepository.Buscar(e => e.IdSituacao == command.IdSituacao).FirstOrDefault();

            if (situacao == null)
                AddNotification("Cod Situação", "O Código de Situação informado não existe");


            
            //Verificar se o Codigo ou Nome informado já está em Uso
            if (_empresaRepository.ChecarSeEmpresaExiste((int)EAcao.Atualizar, command.CodEmpresa, command.Nome, command.Cnpj))
                AddNotification("Código/Nome/CNPJ", "O Código, Nome ou CNPJ já estão em uso");


            //Gerar os VOs (Se houver)
            var cnpj = new CNPJ(command.Cnpj);

            if (cnpj.Invalid)
            {
                AddNotifications(cnpj);
                return new CommandResult(false, "Não foi possível adicionar a empresa");
            }

            
            //Gerar as Entidades
            var empresa = new Empresa(command.CodEmpresa, command.Nome, cnpj, command.IdSituacao);


            //Auditoria
            //var auditoria = new LogAuditoria("Cadastro", empresa.GetType().Name, EAcao.Adicionar, empresa.GetType().Namespace, JsonConvert.SerializeObject(empresa));



            // Relacionamentos (Se houver)



            // Agrupar as Validações
            AddNotifications(empresa);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _empresaRepository.Atualizar(empresa);



            //_auditoriaRepository.Adicionar(auditoria);



            // Enviar E-mail de boas vindas (Se houver necessidade)



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");

        }



        public ICommandResult Handle(ExcluirEmpresaCommand command)
        {
            
            
            //Verificar se a empresa existe
            var registro = _empresaRepository.Buscar(e => e.CodEmpresa == command.CodEmpresa).FirstOrDefault();



            if (registro == null)
                return new CommandResult(false, "Não foi possível realizar esta operação. O cód. informado não existe");




            var relacionamento = _empresaRepository.ExisteRelacionamento(nameof(command.CodEmpresa).ToString(), command.CodEmpresa.ToString());
            if (relacionamento)
                return new CommandResult(false, "Não foi possível realizar esta operação. Empresa possui vínculo com outras entidades da APP");



            //Auditoria
            //var auditoria = new LogAuditoria("Cadastro", registro.GetType().Name, EAcao.Excluir, registro.GetType().Namespace, JsonConvert.SerializeObject(registro));




            // Agrupar as Validações
            AddNotifications(registro);



            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar esta operação");


            // Salvar as Informações
            _empresaRepository.RemoverString(command.CodEmpresa);



            //_auditoriaRepository.Adicionar(auditoria);



            // Retornar informações
            return new CommandResult(true, "Operação realizada com sucesso");

        }
    }
}
