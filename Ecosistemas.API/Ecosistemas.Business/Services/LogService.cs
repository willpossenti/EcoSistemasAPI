
using Ecosistemas.Business.Contexto;
using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Services
{


    public class LogService : BaseService<Log>, ILogService
    {
        private CatalogoDbContext _context;

        public LogService(CatalogoDbContext context) : base(context)
        {
            _context = context;

        }

    }
}
