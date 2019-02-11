using Ecosistemas.Business.Entities;
using Ecosistemas.Business.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecosistemas.Business.Interfaces
{
    public interface IAcessoService
    {
        Task<CustomResponse<Acesso>> Adicionar(Acesso acesso);
    }
}
