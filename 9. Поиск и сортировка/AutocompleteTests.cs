using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Autocomplete
{
    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void GetCountByPrefixTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var expectedResult = 3;
            var result = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetCountByPrefixEmptyTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "c";
            var expectedResult = 0;
            var result = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTest()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var count = 2;
            var expectedResult = new[] { "a", "ab" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTestLessThanCount()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "a";
            var count = 4;
            var expectedResult = new[] { "a", "ab", "abc" };
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void GetTopByPrefixTestEmpty()
        {
            var phrases = new List<string> { "a", "ab", "abc", "b" };
            var prefix = "c";
            var count = 2;
            var expectedResult = new string[0];
            var result = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            Assert.AreEqual(expectedResult, result);
        }
    }



    internal class AutocompleteTask
    {      
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            else
                return null;
        }


        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            int actualCount = Math.Min(GetCountByPrefix(phrases, prefix), count);
            int startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            string[] result = new string[actualCount];
            Array.Copy(phrases.ToArray(), startIndex, result, 0, actualCount);
            return result;
        }


        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            int left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            int right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return right - left - 1;
        }
    }
}