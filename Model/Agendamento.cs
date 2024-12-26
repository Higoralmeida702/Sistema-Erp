using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System_Erp.Enum;

namespace System_Erp.Model
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public virtual UsuarioModel Paciente { get; set; }

        public int MedicoId { get; set; }

        [ForeignKey("MedicoId")]
        public virtual UsuarioModel Medico { get; set; }

        public DateTime DataHora { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}