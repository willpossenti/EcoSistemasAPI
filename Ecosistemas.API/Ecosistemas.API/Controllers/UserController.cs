using Ecosistemas.API.Business;
using Ecosistemas.API.Data;
using Ecosistemas.API.Model;
using Ecosistemas.API.Security;
using Ecosistemas.API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecosistemas.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UserController : Controller
    {
        private UserService _service;

        public UserController(CatalogoDbContext context)
        {
            _service = new UserService(context);
        }

        [HttpPost]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Post([FromBody]User user, [FromServices]AccessManager accessManager)
        {
            return _service.Incluir(user, accessManager, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Put([FromBody]User user, [FromServices]AccessManager accessManager)
        {
            return _service.Alterar(user, accessManager, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpDelete("{UserId}")]
        [Authorize(Roles = Roles.ROLE_API_MASTER)]
        public Task<CustomResponse<User>> Delete(string UserId, [FromBody]User user)
        {
            return _service.Remover(Guid.Parse(UserId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<IList<User>>> Get()
        {
            return _service.BuscarTodosUsers();
        }

        [HttpGet("{UserId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_USUARIOS + "")]
        public Task<CustomResponse<User>> Get(string UserId)
        {
            return _service.BuscarUser(Guid.Parse(UserId));
        }


    }
}
