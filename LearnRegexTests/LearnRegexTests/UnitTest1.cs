using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnRegexTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = "(02) -Fish Go Deep & Tracey K- Cure And The Cause.mp3";

            string pattern = @"([^()0-9 ])+";
            var matcher = new Regex(pattern);

            //Act
            MatchCollection matchCollection = matcher.Matches(text);
            var result = matchCollection.MatchToString();
            var cleanup = CleanUp(result);
            //Assert
            result.Should().Be("Fish Go Deep Tracey Cure And The Cause.");
        }

        private string CleanUp(string result)
        {
            var clean = result.TrimStart('-');

            return clean.Replace("-", " -");
            
        }
    }

    public static class Extensions
    {
        public static string MatchToString(this MatchCollection matchCollection)
        {
            return string.Join(" ", matchCollection.Cast<Match>().Select(x => x.Value));
        }

    }
}
