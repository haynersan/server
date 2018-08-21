#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Shift.Domain.Core.Interfaces;

#endregion


namespace Shift.Services.Api.Configurations
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {

        protected Guid UsuarioDaAplicacao { get; set; }


        private readonly IUnitOfWork _uow;


        public BaseController(IUnitOfWork uow, IUser user)
        {
            _uow = uow;

 
            if (user.IsAuthenticated())
            {
                UsuarioDaAplicacao = user.GetUserId();
            }
        }



        public new IActionResult Response(object result, IEnumerable<Notification> notifications)
        {
       

            if (!notifications.Any())
            {

                try
                {
                    _uow.Commit();

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
    }
}
