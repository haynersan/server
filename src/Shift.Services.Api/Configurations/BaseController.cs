#region usings

using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

#endregion


namespace Shift.Services.Api.Configurations
{
    [Produces("application/json")]
    public class BaseController : Controller
    {

        private readonly Notification _notifications;

        public BaseController()
        {
            //Testes.....Testes....Testes
            //Testes.....Testes....Testes
            //Testes.....Testes....Testes

            //Testes.....Testes....Testes
            //Testes.....Testes....Testes
            //Testes.....Testes....Testes
        }


        public new IActionResult Response(object result, IEnumerable<Notification> notifications)
        {
       

            if (!notifications.Any())
            {

                try
                {
                    //_uow.Commit();

                    return Ok(new
                    {
                        success = true,
                        data = result
                    });
                }
                catch
                {

                    // Logar o erro (Elmah)
                    return BadRequest(new
                    {
                        success = false,
                        errors = new[] { "Ocorreu uma falha interna no servidor." }
                    });
                }

            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = notifications
                });
            }
        }



        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                //NotificarErro(result.ToString(), error.Description);
            }
        }

    }
}
