using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Dto;
using System_Erp.Enum;

namespace System_Erp.Services
{
    public interface IAgendamentoService
    {
        Task<List<InfoMedicoDto>> ObterMedicosPorCargo (EspecialidadeMedica especialidadeMedica);
        Task<List<DateTime>> ObterDiasDisponiveis (int medicoId, int diasAntecipacao = 30);
        Task<bool> AgendarConsulta (AgendamentoDto agendamentoDto);
        Task<bool> CancelarConsulta(int agendamentoId);
    }
}