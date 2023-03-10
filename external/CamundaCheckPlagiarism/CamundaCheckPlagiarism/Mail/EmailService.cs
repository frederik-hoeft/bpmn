using System.Net;
using System.Net.Mail;

namespace CamundaCheckPlagiarism.Mail;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(EmailConfiguration emailConfig)
    {
        _emailConfig = emailConfig;
    }

    public void SendEmail(EmailMessage message)
    {
        MailMessage emailMessage = CreateEmailMessage(message);
        Send(emailMessage);
    }

    private MailMessage CreateEmailMessage(EmailMessage message)
    {
        MailMessage emailMessage = new()
        {
            From = new MailAddress(_emailConfig.From),
            Subject = message.Subject,
            IsBodyHtml = true,
            Body = message.Content
        };
        emailMessage.To.AddRange(message.To);
        return emailMessage;
    }

    private void Send(MailMessage mailMessage)
    {
        using SmtpClient client = new(_emailConfig.SmtpServer, _emailConfig.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password),
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        client.Send(mailMessage);
    }
}