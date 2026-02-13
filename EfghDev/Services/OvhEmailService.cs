using EfghDev.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using static EfghDev.Components.Pages.Contact;

namespace EfghDev.Services
{
    public class OvhEmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        // L'adresse où TU veux recevoir les demandes (ton adresse perso/pro)
        private const string AdminEmailAddress = "contact@efgh-dev.fr";

        public OvhEmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendContactEmailAsync(ContactFormModel model)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = $"[CONTACT SITE] Nouveau projet : {model.ProjectType} - {model.Name}",
                    IsBodyHtml = true,
                };

                // Destinataire : C'est TOI
                mailMessage.To.Add(AdminEmailAddress);

                // Reply-To : Si tu cliques sur "Répondre", ça ira au client, pas à toi-même
                if (!string.IsNullOrEmpty(model.Email))
                {
                    mailMessage.ReplyToList.Add(new MailAddress(model.Email, model.Name));
                }

                // Construction du corps du mail (HTML basique)
                mailMessage.Body = $@"
                    <h2>Nouvelle demande de contact</h2>
                    <hr />
                    <p><strong>Nom :</strong> {model.Name}</p>
                    <p><strong>Société :</strong> {model.Company}</p>
                    <p><strong>Email :</strong> {model.Email}</p>
                    <p><strong>Téléphone :</strong> {model.Phone}</p>
                    <p><strong>Type de projet :</strong> {model.ProjectType}</p>
                    <hr />
                    <h3>Message :</h3>
                    <p>{model.Message.Replace("\n", "<br>")}</p>
                ";

                using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = true, // OVH requiert SSL/TLS sur le port 587
                };

                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Log l'erreur ici (Console, Serilog, etc.)
                Console.WriteLine($"ERREUR SMTP: {ex.Message}");
                return false;
            }
        }
    }
}