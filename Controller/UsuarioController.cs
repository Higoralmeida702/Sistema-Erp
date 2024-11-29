using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System_Erp.Dto;
using System_Erp.Services.AuthService;

namespace System_Erp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioAuthService _usuarioAuthService;

        public UsuarioController(IUsuarioAuthService usuarioAuthService)
        {
            _usuarioAuthService = usuarioAuthService;
        }

        [HttpPost("Registrar/Usuario")]
        public async Task<IActionResult> Registrar (RegistrarDto usuarioDto)
        {
            var resposta = await _usuarioAuthService.Registrar(usuarioDto);
            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }

        [HttpPost("Login/Usuario")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var resposta = await _usuarioAuthService.Login(loginDto);
            if (!resposta.Status)
            {
                return BadRequest(resposta);
            }
            return Ok(resposta);
        }

    }
}