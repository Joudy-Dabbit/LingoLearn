using MailKit.Net.Smtp;
using MailKit.Security;
using LingoLearn.Application.Dashboard.Core.Abstractions.Email;
using LingoLearn.Contracts.Email;
using LingoLearn.Infrastructure.Email.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace LingoLearn.Infrastructure.Email;

public sealed class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> maiLSettingsOptions)
    {
        _mailSettings = maiLSettingsOptions.Value;
    }


    public async Task SendEmailAsync(SendEmailRequest sendEmailRequest)
    {
        var email = new MimeMessage
        {
            From =
            {
                new MailboxAddress(_mailSettings.SenderDisplayName, _mailSettings.SenderEmail)
            },
            To =
            {
                MailboxAddress.Parse(sendEmailRequest.EmailTo)
            },
            Subject = sendEmailRequest.Subject,
            Body = new TextPart(TextFormat.Text)
            {
                Text = sendEmailRequest.Body
            }
        };
        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_mailSettings.SmtpServer, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_mailSettings.SenderEmail, _mailSettings.SmtpPassword);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}