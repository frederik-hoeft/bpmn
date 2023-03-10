using System.Net.Mail;

namespace CamundaCheckPlagiarism.Mail;

public class EmailMessage
{
    public List<MailAddress> To { get; }

    public string Subject { get; }

    public string Content { get; }

    public EmailMessage(IEnumerable<(string Name, string Email)> to, string subject, string content)
    {
        To = new List<MailAddress>();
        To.AddRange(to.Select(x => new MailAddress(x.Email, x.Name)));
        Subject = subject;
        Content = content;
    }

    public EmailMessage(string toName, string toEmail, string subject, string content)
    {
        To = new List<MailAddress>()
        {
            new MailAddress(toEmail, toName),
        };
        Subject = subject;
        Content = content;
    }
}