using CamundaCheckPlagiarism.Checker.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace CamundaCheckPlagiarism.Checker;

public static class PlagiarismChecker
{
    public static double EvaluateContent(string content)
    {
        KnownData knownData = KnownDataStore.GetKnownData();

        int matchingCharacters = 0;

        foreach (KnownDataSet set in knownData.DataSets)
        {
            Regex sentenceFragmentsMatcher = new(@"([^,.?!]+)[,.?!]+");
            foreach (Match sentenceFragmentMatch in sentenceFragmentsMatcher.Matches(set.Content).ToList())
            {
                string sentenceFragment = sentenceFragmentMatch.Groups[1].Value.Trim();
                if (content.Contains(sentenceFragment, StringComparison.InvariantCultureIgnoreCase))
                {
                    matchingCharacters += sentenceFragment.Length;
                }
            }
        }

        double plagiarismScore = (double)matchingCharacters / content.Length;

        return plagiarismScore;
    }
}
