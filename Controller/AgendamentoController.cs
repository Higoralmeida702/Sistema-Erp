using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System_Erp.Dto;
using System_Erp.Enum;
using System_Erp.Services;

namespace System_Erp.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamento;

        public AgendamentoController(IAgendamentoService agendamento)
        {
            _agendamento = agendamento;
        }

        [HttpGet("medicos/{especialidadeMedica}")]
        [Authorize]
        public async Task<ActionResult<List<InfoMedicoDto>>> ObterMedicosPorCargo (EspecialidadeMedica especialidadeMedica)
        {
            var medicos = await _agendamento.ObterMedicosPorCargo(especialidadeMedica);
            if (medicos == null || medicos.Count == 0)
            {
                return NotFound("Nenhum médico encontrado para para o cargo solicitado");
            }
            return Ok(medicos);
        }

        [HttpGet("DiasDisponiveis/{medicoId}")]
        [Authorize]
        public async Task<ActionResult<List<DateTime>>> ObterDiasDisponiveis (int medicoId, int diasAntecipacao = 30)
        {
            var diasDisponiveis = await _agendamento.ObterDiasDisponiveis(medicoId, diasAntecipacao);
            if (diasDisponiveis == null || diasDisponiveis.Count == 0)
            {
                return NotFound("Nenhum dia disponível encontrado para o médico especificado.");
            } 
            return Ok(diasDisponiveis);
        }

        [HttpPost("agendar")]
        [Authorize]
        public async Task<ActionResult> AgendarConsulta (AgendamentoDto agendamentoDto)
        {
            if (agendamentoDto == null)
            {
                return BadRequest("Os dados do agendamento são inválidos.");
            }

            var resultado = await _agendamento.AgendarConsulta(agendamentoDto);
            if (!resultado)
            {
                return BadRequest("Não foi possível agendar a consulta. Verifique se o horário está disponível ou se o intervalo de 40 minutos foi respeitado.");
            }

            return Ok("Consulta agendada com sucesso.");
        }

        [HttpPost("cancelar/{agendamentoId}")]
        [Authorize(Roles = "Administrador,Medico")]

        public async Task<ActionResult> CancelarConsulta(int agendamentoId)
        {
            var resultado = await _agendamento.CancelarConsulta(agendamentoId); // 

            if (!resultado)
            {
                return BadRequest("Não foi possível cancelar o agendamento."); 
            }

            return Ok("Consulta cancelada e notificação enviada ao paciente.");
    }
}
}