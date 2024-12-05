using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using System_Erp.Data;
using System_Erp.Enum;
using System_Erp.Model;

namespace System_Erp.Services
{
    public class SolicitacoesCargoService : ISolicitacoesCargoService
    {
        private readonly ApplicationDbContext _context;

        public SolicitacoesCargoService (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Resposta<string>> AprovarEspecilidadeMedica (int especialidadeId, bool aprovado)
        {
            var solicitacao = await _context.SolicitacoesEspecialidadeMedica.FindAsync(especialidadeId);
            if (solicitacao == null)
            {
                return new Resposta<string> { Status = false, Mensagem = "Solicitação não encontrada"};
            }
            solicitacao.Status = aprovado ? Status.Aprovado : Status.Rejeitado;

            if (aprovado) 
            {
                var usuario = await _context.Usuarios.FindAsync(solicitacao.UsuarioId);
                if (usuario != null)
                {
                    usuario.EspecialidadeDoMedico = solicitacao.CargoSolicitado; 
                }

                _context.SolicitacoesEspecialidadeMedica.Update(solicitacao);
                await _context.SaveChangesAsync();
            }
                return new Resposta<string> { Status = true, Mensagem = aprovado ? "Solicitação aprovada" : "Solicitação rejeitada"};
        }

        public async Task<Resposta<string>> AprovarSolicitacaoNovoCargo(int solicitacaoId, bool aprovado)
        {
            var solicitacao = await _context.SolicitacoesDeCargos.FindAsync(solicitacaoId);
            if (solicitacao == null)
            {
                return new Resposta<string> { Status = false, Mensagem = "Solicitação não encontrada"};
            }

            solicitacao.Status = aprovado ? Status.Aprovado : Status.Rejeitado;

            if (aprovado) 
            {
                var usuario = await _context.Usuarios.FindAsync(solicitacao.UsuarioId);
                if (usuario != null)
                {
                    usuario.CargoDoUsuario = solicitacao.CargoSolicitado;
                }
            }

            _context.SolicitacoesDeCargos.Update(solicitacao);
            await _context.SaveChangesAsync();
            return new Resposta<string> { Status = true, Mensagem = aprovado ? "Solicitação aprovada" : "Solicitação rejeitada"};
        }

        public async Task<List<SolicitacaoDeCargo>> ListarSolicitacoesCargo()
        {
            return await _context.SolicitacoesDeCargos.ToListAsync();
        }

        public async Task<Resposta<string>> SolicitarEspecialidadeMedica (int usuarioId, EspecialidadeMedica especialidadeMedica)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return new Resposta<string> { Status = false, Mensagem = "Usuário não encontrado"};
            }

            if (usuario.CargoDoUsuario != CargoUsuario.Medico)
            {
                return new Resposta<string> { Status = false, Mensagem = "Apenas médicos podem solicitar cargos clínicos" };
            }

            var solicitacao = new SolicitacaoEspecialidadeMedica
            {
                UsuarioId = usuarioId,
                Status = Status.Pendente,
                CargoSolicitado =  especialidadeMedica
            };

            await _context.AddAsync(solicitacao);
            await _context.SaveChangesAsync();

            return new Resposta<string> {Status = true, Mensagem = "Solicitação enviada com sucesso"};
        }

        public async Task<Resposta<string>> SolicitarMudancaCargo(int usuarioId, CargoUsuario novoCargo)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return new Resposta<string> { Status = false, Mensagem = "Usuário não encontrado"};
            }

            var solicitacao = new SolicitacaoDeCargo
            {
                UsuarioId = usuarioId,
                CargoSolicitado = novoCargo,
                Status = Status.Pendente
            };

            _context.SolicitacoesDeCargos.Add(solicitacao);
            await _context.SaveChangesAsync();

            return new Resposta<string> {Status = true, Mensagem = "Solicitação enviada"};
        }
    }
}