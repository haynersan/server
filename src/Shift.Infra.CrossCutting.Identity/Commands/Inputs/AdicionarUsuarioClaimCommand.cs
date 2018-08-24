#region usings

using System;
using Flunt.Validations;
using Shift.Domain.Core.Interfaces;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class AdicionarUsuarioClaimCommand : BaseUsuarioClaimCommand, ICommandResult
    {
        public AdicionarUsuarioClaimCommand(Guid userId, string claimType, string claimValue)
        {
            UserId      = userId;

            ClaimType   = claimType;

            ClaimValue  = claimValue;
        }


        //Fail Fast Validations
        public void Validar()
        {
            AddNotifications(new Contract()

            .Requires()

            .IsNotNull(UserId, "Usuario", "O usuário deve ser informado")
  

            .IsNotNullOrEmpty(ClaimType, "Tipo de Claim", "O Tipo de Claim deve ser informado")


            .IsNotNullOrEmpty(ClaimValue, "Valor Claim", "O valor da Claim deve ser informada")
            );

        }
    }
}
