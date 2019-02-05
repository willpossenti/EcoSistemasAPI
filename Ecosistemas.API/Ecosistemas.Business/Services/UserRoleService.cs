using Ecosistemas.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ecosistemas.Business.Contexto;

namespace Ecosistemas.Business.Services
{

    public class UserRoleService: BaseService<UserRole>
    {
        private CatalogoDbContext _context;

        public UserRoleService(CatalogoDbContext context)  : base (context)
        {
            _context = context;
           
        }

       

    }
}
