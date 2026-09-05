using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Autenticacion
{
    public interface IJwtService
    {
        string GenerateToken(CDUser user);
    }
}
