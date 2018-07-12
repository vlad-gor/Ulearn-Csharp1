using System;
using System.Collections.Generic;
using System.Globalization;


namespace TextAnalysis
{
    static class SentencesParserTask
    {
        static char[] endSimbol = new char[] { '.', '!', '?', ';', ':', '(', ')' };

        public static readonly string[] StopWords =
        {
            "the", "and", "to", "a", "of", "in", "on", "at", "that",
            "as", "but", "with", "out", "for", "up", "one", "from", "into"
        };

        public static List<List<string>> ParseSentences(string text)
        {
            text = text.ToLower();
            List<List<string>> list = new List<List<string>>();
            string[] textSeparation = text.Split(endSimbol);

            foreach (var word in textSeparation)
            {
                List<string> tempList = new List<string>();
                var textSentence = SeparateSimbol(word);

                string[] wrd = textSentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var VARIABLE in wrd)
                {
                    if (BadWord(VARIABLE) && VARIABLE != "")
                        tempList.Add(VARIABLE);
                }

                if (tempList.Count == 0) continue;
                else list.Add(tempList);
            }

            return list;
        }


        private static string SeparateSimbol(string word)
        {
            var textSentence = "";

            foreach (var simbol in word)
            {
                if (char.IsLetter(simbol) || (simbol == '\''))
                    textSentence = textSentence + simbol;
                else textSentence = textSentence + ' ';
            }
            return textSentence;
        }



        public static bool BadWord(string sentence)
        {
            foreach (var wrd in StopWords)
                if (wrd == sentence)
                    return false;
            return true;
        }
    }
}