#region usings

using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Shift.Infra.CrossCutting.Identity.Context;

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

    }
}
