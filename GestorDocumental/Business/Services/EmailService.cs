using System;
using System.IO.Pipelines;
using System.Threading.Tasks;
using GestorDocumental.Business.Interfaces;
using Microsoft.Extensions.Configuration;
using PostmarkDotNet;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
    {
        string apiToken = _configuration["Postmark:ApiToken"];
        string from = _configuration["Postmark:From"];
        string fromName = _configuration["Postmark:FromName"];

        var client = new PostmarkClient(apiToken);

        var message = new PostmarkMessage
        {
            From = $"{fromName} <{from}>",
            To = destinatario,
            Subject = asunto,
            TextBody = cuerpo,
            HtmlBody = $"<p>{cuerpo}</p>"
        };

        try
        {

            var sendResult = await client.SendMessageAsync(message);
            if (sendResult.Status != PostmarkStatus.Success)
            {
                //throw new Exception($"Error al enviar correo: {sendResult.Message}. Código de estado: {sendResult.Status}");
                Console.WriteLine($"Error al enviar correo: {sendResult.Message}. Código de estado: {sendResult.Status}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar correo:usuario invalido");
        }

        
    }
}
