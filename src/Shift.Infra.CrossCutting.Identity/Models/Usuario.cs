#region usings

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

#endregion

namespace Shift.Infra.CrossCutting.Identity.Models
{
    public class Usuario : IdentityUser<Guid>
    {


        //[Required(ErrorMessage = "O código da Matrícula é obrigatório", AllowEmptyStrings = false)]
        [StringLength(06, MinimumLength = 06)]
        public string Matricula { get; set; }

        
        public bool Excluido { get; set; }
    }
}
