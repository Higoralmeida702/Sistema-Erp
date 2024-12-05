using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_Erp.Enum;
using System_Erp.Services;

namespace System_Erp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitacoesController : ControllerBase
    {
        private readonly ISolicitacoesCargoService _iSolicitacoes;

        public SolicitacoesController(ISolicitacoesCargoService iSolicitacoes)
        {
            _iSolicitacoes = iSolicitacoes;
        }

        [HttpPost("solicitarMudancaCargo")]
        [Authorize(Roles = "Administrador,Medico,Paciente")]
        public async Task<IActionResult> SolicitarMudancaDeCargo(int usuarioId, CargoUsuario novoCargo)
        {
            var response = await _iSolicitacoes.SolicitarMudancaCargo(usuarioId, novoCargo);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("aprovarMudancaCargo/{solicitacaoId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AprovarSolicitacaoNovoCargo(int solicitacaoId, bool aprovado)
        {
            var response = await _iSolicitacoes.AprovarSolicitacaoNovoCargo(solicitacaoId, aprovado);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("solicitar-EspecialidadeMedica")]
        [Authorize(Roles = "Administrador, Medico")]
        public async Task<IActionResult> SolicitarEspecialidadeMedica(int usuarioId, EspecialidadeMedica especialidadeMedica)
        {
            var response = await _iSolicitacoes.SolicitarEspecialidadeMedica(usuarioId, especialidadeMedica);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("aprovarEspecialidadeMedica/{especialidadeId}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AprovarEspecialidadeMedica(int especialidadeId, bool aprovado)
        {
            var response = await _iSolicitacoes.AprovarEspecilidadeMedica(especialidadeId, aprovado);
            return response.Status ? Ok(response) : BadRequest(response);
        }


        [HttpGet("listar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarSolicitacoes()
        {
            var solicitacoes = await _iSolicitacoes.ListarSolicitacoesCargo();
            return Ok(solicitacoes);
        }
    }
}