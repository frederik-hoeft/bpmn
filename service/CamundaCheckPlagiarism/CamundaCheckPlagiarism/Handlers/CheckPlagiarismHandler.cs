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
        if (!externalTask.TryGetVariable(CONTENT_VARIABLE_NAME, out string? content, out BpmnErrorResult? error))
        {
            return error;
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
