#region usings

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
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



        public bool checarSeUsuarioExiste(int acao, Guid? id, string userName, string matricula)
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



        public bool checarSeIdEhValido(Guid id)
        {

            return Db.Database.GetDbConnection().Query<bool>("[Usuario].[SP_UsuarioIdEhValido]",
                   new
                   {
                       Id = id.ToString()
                   },
                   commandType: CommandType.StoredProcedure).FirstOrDefault();

        }

        public Usuario ObterUsuario(Guid id)
        {
            return Db.Database.GetDbConnection().Query<Usuario>("[Usuario].[SP_UsuarioListar]",
               new { Id = id },
               commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
