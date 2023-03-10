using Camunda.Worker;
using CamundaCheckPlagiarism.Checker;

namespace CamundaCheckPlagiarism.Handlers;

[HandlerTopics("checkPlagiarism", LockDuration = 10000)]
[HandlerVariables(CONTENT_VARIABLE_NAME)]
public class CheckPlagiarismHandler : IExternalTaskHandler
{
    private const string CONTENT_VARIABLE_NAME = "field_content";

    public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken) => 
        Task.FromResult(Handle(externalTask));

    private static IExecutionResult Handle(ExternalTask externalTask)
    {
        if (externalTask.Variables?.TryGetValue(CONTENT_VARIABLE_NAME, out Variable? contentVariable) is not true || contentVariable.Value is null)
        {
            return new BpmnErrorResult("NO_CONTENT", $"Could not retrieve variable '{CONTENT_VARIABLE_NAME}' or value was null!");
        }
        if (contentVariable.Value is not string content)
        {
            return new BpmnErrorResult("INCORRECT_CONTENT_TYPE", $"Exprected variable '{CONTENT_VARIABLE_NAME}' to be of type {typeof(string).Name}, but found {contentVariable.Value.GetType().Name}!");
        }

        double percentage = PlagiarismChecker.EvaluateContent(content);

        return new CompleteResult
        {
            Variables = new Dictionary<string, Variable>
            {
                ["plagiarism_percentage"] = Variable.Double(percentage)
            }
        };
    }
}
