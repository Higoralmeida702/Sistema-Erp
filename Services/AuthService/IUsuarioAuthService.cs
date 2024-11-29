using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using System_Erp.Dto;
using System_Erp.Model;

namespace System_Erp.Services.AuthService
{
    public interface IUsuarioAuthService
    {
        Task<Resposta<RegistrarDto>> Registrar (RegistrarDto registrarDto);
        Task<Resposta<string>> Login (LoginDto loginDto);
    }
}