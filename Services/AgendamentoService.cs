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

                 var paciente = await _context.Usuarios.FindAsync(agendamentoDto.PacienteId);
            var medico = await _context.Usuarios.FindAsync(agendamentoDto.MedicoId);

               
            if (paciente == null || medico == null)
            {
                throw new Exception("Paciente ou Médico não encontrado.");
            }
                
            if (consultaExistente)
            {
                return false;
            }

            var agendamento = new Agendamento
            {
                PacienteId = agendamentoDto.PacienteId,
                MedicoId = agendamentoDto.MedicoId,
                DataHora = agendamentoDto.DataHora,
                Especialidade = agendamentoDto.Especialidade,
                Status = Status.EmAndamento,
                Paciente = paciente,
                Medico = medico
            };

           
         
            
            await _emailService.EnviarEmail(
            agendamento.Paciente.Email, 
            "Notificação de Confirmação de consulta", 
            $"Prezado(a) {agendamento.Paciente.Nome}, gostariamos de informar que sua consulta de {agendamento.Especialidade} foi confirmada com sucesso, com o(a) Dr(a) {agendamento.Medico.Nome} \n\n" +
            $"Estaremos esperando sua presença no dia {agendamento.DataHora}.\n\n" +
            "Atenciosamente,\n" +
            "[Projeto Academico]" 
            );
            
            await _emailService.EnviarEmail(
            agendamento.Medico.Email, 
            "Notificação de Confirmação de consulta", 
            $"Prezado(a) {agendamento.Medico.Nome}, gostariamos de informar que uma consulta foi confirmada para a paciente {agendamento.Paciente.Nome} para a especialidade {agendamento.Especialidade} com sucesso\n\n" +
            $"Estaremos esperando sua presença no dia {agendamento.DataHora}.\n\n" +
            "Atenciosamente,\n" +
            "[Projeto Academico]" 
            );

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
            $"Informamos que, infelizmente, a consulta de {agendamento.Especialidade} agendada com o(a) Dr(a). {agendamento.Medico.Nome} foi cancelada. Pedimos desculpas por qualquer transtorno que isso possa causar. Caso necessite de esclarecimentos adicionais ou deseje reagendar a consulta, por favor, entre em contato conosco por meio dos nossos canais de atendimento.\n\n" +
            "Agradecemos pela compreensão.\n\n" +
            "Atenciosamente,\n" +
            "[Projeto Acadêmico]"
            );

        await _emailService.EnviarEmail(
            agendamento.Medico.Email, 
            "Notificação de Cancelamento de Consulta", 
            $"Prezado(a) Dr(a). {agendamento.Medico.Nome},\n\n" +
            $"Informamos que, infelizmente, a consulta de {agendamento.Especialidade} agendada com o(a) paciente {agendamento.Paciente.Nome} foi cancelada. Pedimos desculpas por qualquer transtorno que isso possa causar. Caso necessite de esclarecimentos adicionais, por favor, entre em contato conosco por meio dos nossos canais de atendimento.\n\n" +
            "Agradecemos pela compreensão.\n\n" +
            "Atenciosamente,\n" +
            "[Projeto Acadêmico]"
            );

            agendamento.Status = Status.Cancelado;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}