#region usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Infra.CrossCutting.Identity.Commands.Results;
using Shift.Infra.CrossCutting.Identity.Context;
using Shift.Infra.CrossCutting.Identity.Models;

#endregion


namespace Shift.Infra.CrossCutting.Identity.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {


        #region Configuracao

        protected IdentityContext Db;


        public UsuarioRepository(IdentityContext context)
        {
            Db = context;
        }


        #endregion


        #region Validacoes

        public bool ChecarSeUsuarioExiste(int acao, Guid? id, string userName, string matricula)
        {
            return Db.Database.GetDbConnection().Query<bool>("[Usuario].[SP_UsuarioChecar]",
                   new
                        {   Acao        = acao,
                            Id          = id,
                            Username    = userName,
                            Matricula   = matricula
                       },
                       commandType: CommandType.StoredProcedure).FirstOrDefault();
        }



        public bool ChecarSeIdEhValido(Guid id, bool excluido)
        {

            return Db.Database.GetDbConnection().Query<bool>("[Usuario].[SP_UsuarioIdEhValido]",
                   new
                   {
                       Id       = id.ToString(),

                       Excluido = excluido
                   },
                   commandType: CommandType.StoredProcedure).FirstOrDefault();

        }



        public bool ChecarSeUsuarioMovimenta(Guid id)
        {

            return Db.Database.GetDbConnection().Query<bool>("[Usuario].[SP_UsuarioMovimenta]",
                   new
                   {
                       Id = id.ToString()
                   },
                   commandType: CommandType.StoredProcedure).FirstOrDefault();
        }



        public bool ChecarSeUsuarioClaimExiste(Guid userId, string claimType, string claimValue)
        {
            return Db.Database.GetDbConnection().Query<bool>("[Usuario].[SP_UsuarioClaimChecar]",
                 new
                 {
                     UserId     = userId,
                     ClaimType  = claimType,
                     ClaimValue = claimValue
                 },
                     commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        #endregion


        #region Leitura


        public Usuario ObterUsuario(Guid id, bool excluido)
        {
            return Db.Database.GetDbConnection().Query<Usuario>("[Usuario].[SP_UsuarioListarPorId]",
               new
               {

                   Id = id,

                   Excluido = excluido
               },
               commandType: CommandType.StoredProcedure).FirstOrDefault();
        }



        public IEnumerable<UsuarioCommandResult> ListarUsuarios(bool excluido)
        {
            return Db.Database.GetDbConnection().Query<UsuarioCommandResult>("[Usuario].[SP_UsuarioListar]",
                  new
                  {
                      Excluido = excluido
                  },
                    commandType: CommandType.StoredProcedure).ToList();

        }


        public IEnumerable<UsuarioCommandResult> ListarUsuariosPaginados(int pagina, int qtdeItensPorPagina, string nomeUsuario, bool excluido)
        {
            return Db.Database.GetDbConnection().Query<UsuarioCommandResult>("[Usuario].[SP_UsuarioListar]",
                  new
                  {
                      UserName = nomeUsuario,

                      Excluido = excluido
                  },
                    commandType: CommandType.StoredProcedure).ToList().Skip(qtdeItensPorPagina * (pagina - 1)).Take(qtdeItensPorPagina);

        }



        public IEnumerable<UsuarioCommandResult> TotalizarUsuarios(bool excluido)
        {
            return Db.Database.GetDbConnection().Query<UsuarioCommandResult>("[Usuario].[SP_UsuarioListar]",
                  new
                  {
                      Excluido = excluido
                  },
                commandType: CommandType.StoredProcedure).ToList();
        }



        public IEnumerable<UsuarioClaimCommandResult> ListarUsuarioClaims(int pagina, int qtdeItensPorPagina, string nomeUsuario)
        {

            return Db.Database.GetDbConnection().Query<UsuarioClaimCommandResult>("[Usuario].[SP_UsuarioClaimListar]",
                    new { UserName = nomeUsuario },
                        commandType: CommandType.StoredProcedure).ToList().Skip(qtdeItensPorPagina * (pagina - 1)).Take(qtdeItensPorPagina);

        }



        public IEnumerable<UsuarioClaimCommandResult> TotalizarUsuarioClaims()
        {
            return Db.Database.GetDbConnection().Query<UsuarioClaimCommandResult>("[Usuario].[SP_UsuarioClaimListar]",
                        commandType: CommandType.StoredProcedure).ToList();
        }


        #endregion

    }
}
