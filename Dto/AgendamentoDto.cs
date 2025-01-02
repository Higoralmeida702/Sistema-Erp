using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System_Erp.Enum;

namespace System_Erp.Dto
{
    public class AgendamentoDto
    {
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }
        public EspecialidadeMedica Especialidade { get ; set; }
    }
}