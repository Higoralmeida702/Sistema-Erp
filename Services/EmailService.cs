using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace System_Erp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmail(string emailDestino, string assunto, string mensagem)
        {
            try
            {
                var smtpHost = _configuration["SmtpSettings:Host"];
                var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
                var smtpUsername = _configuration["SmtpSettings:Username"];
                var smtpPassword = _configuration["SmtpSettings:Password"];

                var fromAddress = new MailAddress(smtpUsername, "Projeto Academico");
                var toAddress = new MailAddress(emailDestino);
                string subject = assunto;
                string body = mensagem;

                using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar o e-mail: {ex.Message}");
                throw;
            }
        }
    }
}