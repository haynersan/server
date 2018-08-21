#region usings

using System;
using Flunt.Validations;
using Shift.Domain.Core.Interfaces;
using Shift.Domain.Core.Utils;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class EditarUsuarioCommand : BaseUsuarioCommand, ICommandResult
    {


        public EditarUsuarioCommand(Guid id, string username, string email, string matricula, string phoneNumber)
        {

            Id              = id;

            UserName        = TratandoStrings.RemoverAcentuacao(TratandoStrings.RemoverEspacosEntrePalavras(username));

            Email           = email;

            Matricula       = matricula;

            PhoneNumber     = phoneNumber;

        }



        //Fail Fast Validations
        public void Validar()
        {
            AddNotifications(new Contract()

            .Requires()


            .IsNotNull(Id, "Id", "O código do usuário deve ser informado")

            .IsNotNullOrEmpty(UserName, "Usuario", "O usuário deve ser informado")
            .HasMaxLen(UserName, 50, "Usuario", "O usuário deve possuir no máximo 50 caracteres")


            .IsNotNullOrEmpty(Email, "Email", "O email deve ser informado")
            .IsEmail(Email, "Usuario", "O usuário deve possuir no máximo 50 caracteres")


            .IsNotNullOrEmpty(Matricula, "Matricula", "A matrícula deve ser informada")
            .HasLen(Matricula, 06, "Matricula", "A matrícula deve possuir 06 caracteres")
            );

        }
    }
}
