using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System_Erp.Services
{
    public interface IEmailService
    {
        Task EnviarEmail(string emailDestino, string assunto, string mensagem);
    }
}