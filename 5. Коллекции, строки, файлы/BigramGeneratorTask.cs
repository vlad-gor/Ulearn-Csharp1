using System.Collections.Generic;
using System.Text;


namespace TextAnalysis
{
    static class BigramGeneratorTask
    {
        public static string ContinuePhraseWithBigramms
			(Dictionary<string, string> mostFrequentNextWords, string word, int phraseWordsCount)
        {
            StringBuilder phrase = new StringBuilder(word);

            for (int i = 1; i < phraseWordsCount; i++)
            {
                if (!mostFrequentNextWords.ContainsKey(word))
                    break;
                word = mostFrequentNextWords[word];
                phrase.Append(" " + word);
            }
            return phrase.ToString();
        }
    }
}