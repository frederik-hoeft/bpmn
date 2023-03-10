using Camunda.Worker;
using CamundaCheckPlagiarism.Mail;
using System.Globalization;

namespace CamundaCheckPlagiarism.Handlers;

[HandlerVariables(TITLE_VARIABLE_NAME, GRADE_VARIABLE_NAME, MAIL_VARIABLE_NAME, NAME_VARIABLE_NAME)]
public abstract class SendEmailHandlerBase : IExternalTaskHandler
{
    private const string TITLE_VARIABLE_NAME = "field_title";
    private const string GRADE_VARIABLE_NAME = "grade";
    private const string MAIL_VARIABLE_NAME = "field_mail";
    private const string NAME_VARIABLE_NAME = "field_name";

    private readonly IEmailService _emailService;

    protected SendEmailHandlerBase(IEmailService emailService)
    {
        _emailService = emailService;
    }

    protected string Subject { get; } = "Status update on your submission";

    protected abstract string Message { get; }

    public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        => Task.FromResult(Handle(externalTask));

    private IExecutionResult Handle(ExternalTask externalTask)
    {
        if (!externalTask.TryGetVariable(TITLE_VARIABLE_NAME, out string? title, out BpmnErrorResult? error) 
            || !externalTask.TryGetVariable(MAIL_VARIABLE_NAME, out string? mailAddress, out error)
            || !externalTask.TryGetVariable(NAME_VARIABLE_NAME, out string? name, out error))
        {
            return error;
        }

        double grade = externalTask.GetVariableOrDefault(GRADE_VARIABLE_NAME, 5.0d);

        EmailMessage message = new(name, mailAddress, Subject, Message
            .Replace("%GRADE%", grade.ToString("F1", CultureInfo.InvariantCulture))
            .Replace("%NAME%", name)
            .Replace("%TITLE%", title));

        _emailService.SendEmail(message);

        return new CompleteResult();
    }
}
