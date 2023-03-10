using System.Text.Json;

namespace CamundaCheckPlagiarism.Mail;

public record EmailConfiguration(string From, string SmtpServer, int Port, string Username, string Password)
{
    public static EmailConfiguration LoadFromConfigFile()
    {
        const string CONFIG_FILE = "smtp-settings.json";
        using Stream stream = File.OpenRead(CONFIG_FILE);
        return JsonSerializer.Deserialize<EmailConfiguration>(stream)
            ?? throw new FormatException($"{CONFIG_FILE} could not be loaded!");
    }
}
