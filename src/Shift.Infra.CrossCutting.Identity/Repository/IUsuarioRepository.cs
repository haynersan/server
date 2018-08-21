using System;
using System.Collections.Generic;
using Shift.Infra.CrossCutting.Identity.Models;

namespace Shift.Infra.CrossCutting.Identity.Repository
{
    public interface IUsuarioRepository
    {

        #region Validacoes

        bool checarSeUsuarioExiste(int acao, Guid? id, string userName, string matricula);

        bool checarSeIdEhValido(Guid id);

        #endregion

        Usuario ObterUsuario(Guid id);

    }
}
