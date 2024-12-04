using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Enum;
using System_Erp.Model;

namespace System_Erp.Services
{
    public interface  ISolicitacoesCargoService
    {
        Task<Resposta<string>> SolicitarMudancaCargo (int usuarioId, CargoUsuario novoCargo);
        Task<Resposta<string>> AprovarSolicitacaoNovoCargo (int solicitacaoId, bool aprovado);
        Task<Resposta<string>> SolicitarEspecialidadeMedica (int usuarioId, EspecialidadeMedica especialidadeMedica);
        Task<Resposta<string>> AprovarEspecilidadeMedica (int especialidadeId, bool aprovado);

        Task<List<SolicitacaoDeCargo>> ListarSolicitacoesCargo ();
    }
}