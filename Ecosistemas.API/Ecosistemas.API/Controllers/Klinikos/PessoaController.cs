

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ecosistemas.Business;
using Ecosistemas.Business.Entities.Klinikos;
using Ecosistemas.Business.Contexto.Klinikos;
using Ecosistemas.Business.Interfaces.Klinikos;
using Ecosistemas.Business.Services.Klinikos;
using Ecosistemas.Security.Manager;
using Ecosistemas.Business.Utility;

namespace Ecosistemas.API.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class PessoaController : Controller
    {
        private IPessoaService _service;

        public PessoaController(KlinikosDbContext context)
        {
            _service = new PessoaService(context);
        }

        [Route("Incluir")]
        [HttpPost]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public Task<CustomResponse<Pessoa>> Incluir([FromBody]Pessoa pessoa)
        {
            return _service.Adicionar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpPut]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public Task<CustomResponse<Pessoa>> Put([FromBody]Pessoa pessoa, [FromServices]AccessManager accessManager)
        {
            return _service.Atualizar(pessoa, Guid.Parse(HttpContext.User.Identity.Name));
        }


        [HttpDelete("{PessoaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public Task<CustomResponse<Pessoa>> Delete(string PessoaId)
        {
           // return _service.RemoverUser(Guid.Parse(UserId), Guid.Parse(HttpContext.User.Identity.Name));
            return _service.Remover(Guid.Parse(PessoaId), Guid.Parse(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public Task<CustomResponse<IList<Pessoa>>> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{PessoaId}")]
        [Authorize(Roles = "" + Roles.ROLE_API_MASTER + "," + Roles.ROLE_API_KLINIKOS + "")]
        public Task<CustomResponse<Pessoa>> Get(string PessoaId)
        {
            return _service.Obter(Guid.Parse(PessoaId));
        }

      

    }
}
