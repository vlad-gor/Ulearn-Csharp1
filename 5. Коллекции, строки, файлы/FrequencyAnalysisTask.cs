using System.Collections.Generic;
using System;


namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, Dictionary<string, int>> FillBigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    if (!bigrams.ContainsKey(sentence[i]))
                        bigrams.Add(sentence[i], new Dictionary<string, int>());
                    if (!bigrams[sentence[i]].ContainsKey(sentence[i + 1]))
                        bigrams[sentence[i]].Add(sentence[i + 1], 1);
                    else bigrams[sentence[i]][sentence[i + 1]] += 1;
                }
            }

            return bigrams;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = FillBigrams(text);
            Dictionary<string, string> mostFrequentBigrams = new Dictionary<string, string>();
            foreach (var firstWord in bigrams)
            {
                var maxValue = 0;
                string mostFrequentSecondWord = null;

                foreach (var secondWord in firstWord.Value)
                {
                    if (secondWord.Value == maxValue)
                        if (string.CompareOrdinal(mostFrequentSecondWord, secondWord.Key) > 0)
                            mostFrequentSecondWord = secondWord.Key;
                    if (secondWord.Value > maxValue)
                    {
                        mostFrequentSecondWord = secondWord.Key;
                        maxValue = secondWord.Value;
                    }
                }
                mostFrequentBigrams.Add(firstWord.Key, mostFrequentSecondWord);
            }

            return mostFrequentBigrams;
        }

        public static Dictionary<string, Dictionary<string, int>> FillTrigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 2; i++)
                {
                    if (!bigrams.ContainsKey(sentence[i] + " " + sentence[i + 1]))
                        bigrams.Add(sentence[i] + " " + sentence[i + 1], new Dictionary<string, int>());
                    if (!bigrams[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i + 2]))
                        bigrams[sentence[i] + " " + sentence[i + 1]].Add(sentence[i + 2], 1);
                    else bigrams[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]] += 1;
                }
            }

            return bigrams;
        }

        public static Dictionary<string, string> GetMostFrequentNextWordsTrigram(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = FillTrigrams(text);
            Dictionary<string, string> mostFrequentTrigrams = new Dictionary<string, string>();
 
			foreach (var wordPair in bigrams)
            {
                var maxValue = 0;
                string mostFrequentThirdWord = null;
 
				foreach (var ThirdWord in wordPair.Value)
                {
                    if (ThirdWord.Value == maxValue)
                        if (string.CompareOrdinal(mostFrequentThirdWord, ThirdWord.Key) > 0)
                            mostFrequentThirdWord = ThirdWord.Key;
                    if (ThirdWord.Value > maxValue)
                    {
                        mostFrequentThirdWord = ThirdWord.Key;
                        maxValue = ThirdWord.Value;
                    }
                }
                mostFrequentTrigrams.Add(wordPair.Key, mostFrequentThirdWord);
            }
            return mostFrequentTrigrams;
        }
    }
}