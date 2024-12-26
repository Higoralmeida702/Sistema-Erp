using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System_Erp.Data;
using System_Erp.Dto;
using System_Erp.Enum;
using System_Erp.Model;

namespace System_Erp.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AgendamentoService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<bool> AgendarConsulta(AgendamentoDto agendamentoDto)
        {
            var consultaExistente = await _context.Agendamentos
                .AnyAsync(a => a.MedicoId == agendamentoDto.MedicoId && a.DataHora == agendamentoDto.DataHora);
                
            if (consultaExistente)
            {
                return false;
            }

            var agendamento = new Agendamento
            {
                PacienteId = agendamentoDto.PacienteId,
                MedicoId = agendamentoDto.MedicoId,
                DataHora = agendamentoDto.DataHora,
                Status = Status.EmAndamento
            };

            _context.Agendamentos.Add(agendamento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DateTime>> ObterDiasDisponiveis(int medicoId, int diasAntecipacao = 30)
        {
            var diasDiponiveis = new List<DateTime>();
            var dataAtual = DateTime.Today;
            var dataLimite = dataAtual.AddDays(diasAntecipacao);

            var horarioInicio = new TimeSpan(8,0,0);
            var horarioFim = new TimeSpan(18,0,0);
            var intervaloConsulta = TimeSpan.FromMinutes(40);

            for (var dia = dataAtual; dia <= dataLimite; dia = dia.AddDays(1))
            {
                var consultasAgendadas = await _context.Agendamentos
                    .Where(a => a.MedicoId == medicoId && a.DataHora.Date == dia)
                    .ToListAsync();

                    for (var horario = horarioInicio; horario < horarioFim; horario += intervaloConsulta)
                    {
                        var dataHoraConsulta = dia.Add(horario);
                        var consultaJaAgendada = consultasAgendadas.Any(a => a.DataHora == dataHoraConsulta);

                        if (!consultaJaAgendada)
                        {
                            diasDiponiveis.Add(dataHoraConsulta);
                        }
                    }
            }
            return diasDiponiveis;
        }

        public async Task<List<InfoMedicoDto>> ObterMedicosPorCargo(EspecialidadeMedica especialidadeMedica)
        {
            return await _context.Usuarios
                .Where(u => u.EspecialidadeDoMedico.Contains(especialidadeMedica)) 
                .Select(u => new InfoMedicoDto
                {
                    Nome = u.Nome,
                    Sobrenome = u.Sobrenome,
                    Email = u.Email,
                    NumeroTelefone = u.NumeroTelefone,
                    EspecialidadeMedica = string.Join(", ", u.EspecialidadeDoMedico)
                })
                .ToListAsync();
        }

        public async Task<bool> CancelarConsulta(int agendamentoId)
        {
            var agendamento = await _context.Agendamentos
                .Include(a => a.Paciente) 
                .Include(a => a.Medico)
                .FirstOrDefaultAsync(a => a.Id == agendamentoId);

            if (agendamento == null)
            {
                return false;
            }

            await _emailService.EnviarEmail(
            agendamento.Paciente.Email, 
            "Notificação de Cancelamento de Consulta", 
            $"Prezado(a) {agendamento.Paciente.Nome},\n\n" +
            $"Informamos que, infelizmente, a consulta agendada com o(a) Dr(a). {agendamento.Medico.Nome} foi cancelada. Pedimos desculpas por qualquer transtorno que isso possa causar. Caso necessite de esclarecimentos adicionais ou deseje reagendar a consulta, por favor, entre em contato conosco por meio dos nossos canais de atendimento.\n\n" +
            "Agradecemos a compreensão.\n\n" +
            "Atenciosamente,\n" +
            "[Projeto Academico]" 
            );

            agendamento.Status = Status.Cancelado;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}