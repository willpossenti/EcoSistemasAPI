using Ecosistemas.Business.Contexto;
using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Services;
using Ecosistemas.Security.Manager;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Ecosistemas.Security.Manager.Util;

namespace Ecosistemas.API.Initial
{
    public class IdentityInitializer
    {
        private readonly CatalogoDbContext _context;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private readonly AccessManager _acessmanager;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfigurations _tokenConfigurations;

        public IdentityInitializer(CatalogoDbContext context, SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations, IServiceProvider services)
        {
            _context = context;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _acessmanager = new AccessManager(_signingConfigurations, _tokenConfigurations);
        }

        public async void Initialize()
        {

            try
            {
                if (_context.Database.EnsureCreated())
                {

                    //Cria roles
                    var listaRoles = new List<Role>() {
                        new Role() {  RoleId = Guid.NewGuid() , NameRole = Roles.ROLE_API_MASTER  },
                        new Role() { RoleId = Guid.NewGuid() , NameRole = Roles.ROLE_API_USUARIOS }
                    };

                    var _userMaster = new Util.UserMaster();

                    //Cria o usuário Master
                    var _user = new User()
                    {
                        Username = _userMaster.Username,
                        Email = _userMaster.Email,
                        Password =  _userMaster.Password
                    };

                    await new UserService(_context).Adicionar(_user, _acessmanager, _user.UserId);
                    await new RoleService(_context).AdicionarRange(listaRoles, _user.UserId);

                    //Atribui a role master para o usuário master
                    var _role = listaRoles.Where(x => x.NameRole == Roles.ROLE_API_MASTER).FirstOrDefault();

                    var _userRole = new UserRole() { Role = _role, User = _user };

                    await new UserRoleService(_context).Adicionar(_userRole, _user.UserId);

                }
            }
            catch (Exception ex)
            {

                throw new Exception(
                               ex.Message);

            }
        }
    }
}
