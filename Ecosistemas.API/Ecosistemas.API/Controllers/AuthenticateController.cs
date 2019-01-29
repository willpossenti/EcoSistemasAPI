
using Ecosistemas.API.Business;
using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using Ecosistemas.API.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcosistemasAPI.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User usuario,
            [FromServices]AccessManager accessManager)
        {
            var resultado = accessManager.ValidateCredentials(usuario).Result;
            
            if (resultado.StatusCode == StatusCodes.Status200OK)
            {

                    return accessManager.GenerateToken(resultado.Result);
            }
            else
            {
                return new
                {
                    Authenticated = false,
                    resultado.Message,
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }

        

    }
}