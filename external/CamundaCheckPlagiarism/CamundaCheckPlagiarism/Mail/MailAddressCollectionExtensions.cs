using System.Net.Mail;

namespace CamundaCheckPlagiarism.Mail;

public static class MailAddressCollectionExtensions
{
    public static void AddRange(this MailAddressCollection target, IEnumerable<MailAddress> addresses)
    {
        foreach (MailAddress address in addresses)
        {
            target.Add(address);
        }
    }
}
