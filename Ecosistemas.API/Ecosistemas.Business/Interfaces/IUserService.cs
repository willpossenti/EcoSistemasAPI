using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Utility;
using Ecosistemas.Security.Manager;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Ecosistemas.Security.Manager.Util;

namespace Ecosistemas.Business.Interfaces
{
    public interface IUserService
    {
        Task<CustomResponse<User>> Adicionar(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<User>> Alterar(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<IList<User>>> BuscarTodosUsers();
        Task<CustomResponse<User>> BuscarUser(Guid id);
        Task<CustomResponse<User>> ConfirmarSenha(User user, AccessManager accessManager, Guid UserId);
        Task<CustomResponse<User>> RemoverUser(Guid id, Guid UserId);
        Task<CustomResponse<User>> ValidateCredentials(User user, AccessManager accessManager);
        Task<CustomResponse<Token>> GerarAcesso(User user, AccessManager accessManager);
    }
}
