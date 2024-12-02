using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Enum;
using System_Erp.Model;

namespace System_Erp.Services.AuthService
{
    public interface IAdministradorAccountService
    {
        Task<Resposta<string>> AtribuirCargo (int usuarioId, CargoUsuario novoCargo);
        Task InicializarAdm();
    }
}