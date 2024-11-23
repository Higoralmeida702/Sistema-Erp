using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace System_Erp.Enum
{
    public enum CargoUsuario
    {
        [Display (Name = "Paciente")]
        Paciente = 1,

        [Display (Name = "Médico")]
        Medico = 2,

        [Display (Name = "Administrador")]
        Administrador = 3
    }
}