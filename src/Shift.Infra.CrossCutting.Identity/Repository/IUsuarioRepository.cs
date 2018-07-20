using System;

namespace Shift.Infra.CrossCutting.Identity.Repository
{
    public interface IUsuarioRepository
    {

        #region Validacoes

        bool checarSeUsuarioExiste(int acao, Guid? id, string userName, string matricula);

        #endregion

    }
}
