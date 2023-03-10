using System.Text.Json;

namespace CamundaCheckPlagiarism.Checker.Data;

public static class KnownDataStore
{
    private static readonly KnownData _knownData;

    private const string DATA_SOURCE = "data.json";

    static KnownDataStore()
    {
        using Stream dataStream = File.OpenRead(DATA_SOURCE);
        KnownData? knownData = JsonSerializer.Deserialize<KnownData>(dataStream) 
            ?? throw new FormatException($"could not deserialize '{DATA_SOURCE}'.");
        _knownData = knownData;
    }

    public static KnownData GetKnownData() => _knownData;
}
