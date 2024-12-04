using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System_Erp.Enum;

namespace System_Erp.Model
{
    public class SolicitacaoEspecialidadeMedica
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        public EspecialidadeMedica CargoSolicitado { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}