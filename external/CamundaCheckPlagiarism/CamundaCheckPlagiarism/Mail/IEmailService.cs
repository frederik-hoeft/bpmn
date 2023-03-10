namespace CamundaCheckPlagiarism.Mail;

public interface IEmailService
{
    void SendEmail(EmailMessage message);
}