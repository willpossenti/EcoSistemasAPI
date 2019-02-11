
using Ecosistemas.Business;
using Ecosistemas.Business.Contexto;
using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Interfaces;
using Ecosistemas.Business.Services;
using Ecosistemas.Security.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("ApiPolicy")]
    public class AuthenticateController : Controller
    {
        private IUserService _service;

        public AuthenticateController(CatalogoDbContext context)
        {
            _service = new UserService(context);
        }

        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User usuario,
            [FromServices]AccessManager accessManager)
        {
            var resultado = _service.ValidateCredentials(usuario, accessManager).Result;

            if (resultado.StatusCode == StatusCodes.Status200OK)
            {
                accessManager.IpAcess = HttpContext.Connection.LocalIpAddress.ToString() != "127.0.0.1" ? HttpContext.Connection.RemoteIpAddress.ToString() :
                    HttpContext.Connection.LocalIpAddress.ToString();

                return new
                {
                    Authenticated = true,
                    Message = "Acesso autorizado",

                    _service.GerarAcesso(resultado.Result, accessManager).Result,
                    StatusCode = StatusCodes.Status202Accepted
                };

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