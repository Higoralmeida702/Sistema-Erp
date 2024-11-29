using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace System_Erp.Enum
{
    public enum EspecialidadeMedica
    {
        [Display(Name = "Psicólogo")]
        Psicologo = 1,

        [Display(Name = "Nutricionista")]
        Nutricionista = 2,

        [Display(Name = "Dermatologista")]
        Dermatologista = 3,

        [Display(Name = "Clínico Geral")]
        ClinicoGeral = 4
    }
}