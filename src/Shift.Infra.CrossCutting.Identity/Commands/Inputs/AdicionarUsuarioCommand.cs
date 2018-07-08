using Flunt.Validations;

namespace Shift.Infra.CrossCutting.Identity.Commands.Inputs
{
    public class AdicionarUsuarioCommand : BaseUsuarioCommand
    {

        public AdicionarUsuarioCommand(string username, string email, string matricula)
        {

            UserName    = username;

            Email       = email;

            Password    = "abc123";

            Matricula   = matricula;

            #region ContratoDeValidacao

            AddNotifications(new Contract()

                .Requires()

                .IsNotNullOrEmpty(UserName, "Usuario", "O usuário deve ser informado")
                .HasMaxLen(UserName, 50, "Usuario", "O usuário deve possuir no máximo 50 caracteres")


                                
                .IsNotNullOrEmpty(Matricula, "Matricula", "A matrícula deve ser informada")
                .HasLen(Matricula, 06, "Matricula", "A matrícula deve possuir 06 caracteres")
                );

            #endregion

        }
    }
}
