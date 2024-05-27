using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Usuario;

namespace api.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuarios user);
    }
}