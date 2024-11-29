using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System_Erp.Model
{
    public class Resposta<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}