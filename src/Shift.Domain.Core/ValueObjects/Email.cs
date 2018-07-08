using Flunt.Validations;

namespace Shift.Domain.Core.ValueObjects
{
    public class Email : ValueObject
    {
        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;


            AddNotifications(new Contract()
                .IsEmail(Endereco, "Email", "Email Inválido"));

        }


        public string Endereco { get; private set; }
    }
}
