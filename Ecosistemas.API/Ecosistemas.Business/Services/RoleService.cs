using Ecosistemas.Business.Contexto;
using Ecosistemas.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services
{

    public interface IRoleService
    {

    }

    public class RoleService : BaseService<Role>, IRoleService
    {
        private CatalogoDbContext _context;

        public RoleService(CatalogoDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
