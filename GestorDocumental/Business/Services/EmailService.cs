using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GestorDocumental.Business.Interfaces;
using Microsoft.Extensions.Configuration;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
    {
        // Recupera la configuración del correo desde el archivo appsettings.json
        string host = _configuration["Email:Host"];
        int port = int.Parse(_configuration["Email:Port"]);
        string username = _configuration["Email:Username"];
        string password = _configuration["Email:Password"];
        string from = _configuration["Email:From"];

        using (var smtpClient = new SmtpClient(host, port))
        {
            smtpClient.Credentials = new NetworkCredential(username, password);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = asunto,
                Body = cuerpo,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(destinatario);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

