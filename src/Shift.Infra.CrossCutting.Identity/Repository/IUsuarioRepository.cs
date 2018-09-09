#region usings

using System;
using System.Collections.Generic;
using Shift.Infra.CrossCutting.Identity.Commands.Results;
using Shift.Infra.CrossCutting.Identity.Models;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Repository
{
    public interface IUsuarioRepository
    {

        #region Validacoes

        bool ChecarSeUsuarioExiste(int acao, Guid? id, string userName, string matricula, bool excluido);

        bool ChecarSeIdEhValido(Guid id);

        bool ChecarSeUsuarioMovimenta(Guid id);

        bool ChecarSeUsuarioClaimExiste(Guid userId, string claimType, string claimValue);

        #endregion


        #region Leitura


        Usuario ObterUsuario(Guid id, bool excluido);


        IEnumerable<UsuarioCommandResult> ListarUsuarios(bool excluido);


        IEnumerable<UsuarioCommandResult> ListarUsuariosPaginados(int pagina, int qtdeItensPorPagina, string nomeUsuario, bool excluido);


        IEnumerable<UsuarioCommandResult> TotalizarUsuarios(bool excluido);


        IEnumerable<UsuarioClaimCommandResult> ListarUsuarioClaims(int pagina, int qtdeItensPorPagina, string nomeUsuario);


        IEnumerable<UsuarioClaimCommandResult> TotalizarUsuarioClaims();

        
        #endregion

    }
}
