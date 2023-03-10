using Camunda.Worker;
using System.Diagnostics.CodeAnalysis;

namespace CamundaCheckPlagiarism.Handlers;

public static class ExternalTaskExtensions
{
    public static bool TryGetVariable<T>(this ExternalTask externalTask, string variableName, [NotNullWhen(true)] out T? value, [NotNullWhen(false)] out BpmnErrorResult? error)
    {
        if (externalTask.Variables?.TryGetValue(variableName, out Variable? variable) is not true || variable.Value is null)
        {
            error = new BpmnErrorResult("NO_CONTENT", $"Could not retrieve variable '{variableName}' or value was null!");
            goto ERROR;
        }
        if (variable.Value is not T val)
        {
            error = new BpmnErrorResult("INCORRECT_CONTENT_TYPE", $"Exprected variable '{variableName}' to be of type {typeof(string).Name}, but found {variable.Value.GetType().Name}!");
            goto ERROR;
        }
        error = null;
        value = val;
        return true;
    ERROR:
        value = default;
        return false;
    }

    public static T GetVariableOrDefault<T>(this ExternalTask externalTask, string variableName, T defaultValue)
    {
        if (externalTask.Variables?.TryGetValue(variableName, out Variable? variable) is true && variable.Value is T value)
        {
            return value;
        }
        return defaultValue;
    }
}
