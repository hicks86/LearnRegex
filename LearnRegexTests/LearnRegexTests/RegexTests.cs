using System;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearnRegexTests
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void Basic_Regex1()
        {
            //Arrange
            var text = "The fat cat sat on the mat";
            string pattern = "the";
            var matcher = new Regex(pattern);

            //Act
            var result = matcher.Match(text);

            //Assert
            result.Value.Should().Be("the");
        }

        [TestMethod]
        public void Basic_Regex2()
        {
            //Arrange
            var text = "The fat cat sat on the mat";
            string pattern = "The";
            var matcher = new Regex(pattern);

            //Act
            var result = matcher.Match(text);

            //Assert
            result.Value.Should().Be("The");
        }

        [TestClass]
        public class MetaCharacters
        {
            /// <summary>
            /// Full stop matches any single character. It will not match return or newline characters
            ///  For example, the regular expression .ar means: any character, followed by the letter a, followed by the letter r.
            /// </summary>
            [TestMethod]
            public void FullStop()
            {
                //Arrange
                var text = "The car parked in the garage.";
                string pattern = ".ar";
                var matcher = new Regex(pattern);

                //Act
                var result = matcher.Matches(text);

                //Assert
                result.Count.Should().Be(3);
                result[0].Value.Should().Be("car");
                result[1].Value.Should().Be("par");
                result[2].Value.Should().Be("gar");
            }

            /// <summary>
            /// Square brackets are used to specify character sets. Use a hyphen inside a character set to specify the characters' range. 
            /// The order of the character range inside square brackets doesn't matter. 
            /// For example, the regular expression [Tt]he means: an uppercase T or lowercase t, followed by the letter h, 
            /// followed by the letter e.
            /// </summary>
            [TestMethod]
            public void CharacterSet()
            {
                //Arrange
                var text = "The car parked in the garage.";
                string pattern = "[Tt]he";
                var matcher = new Regex(pattern);

                //Act
                var result = matcher.Matches(text);

                //Assert
                result.Count.Should().Be(2);
                result[0].Value.Should().Be("The");
                result[1].Value.Should().Be("the");
            }

            /// <summary>
            ///  When the caret symbol is typed after the opening square bracket it negates the character set. 
            ///  For example, the regular expression [^c]ar means: any character except c, 
            ///  followed by the character a, followed by the letter r.
            /// </summary>
            [TestMethod]
            public void NegatedCharacterSet()
            {
                //Arrange
                var text = "The car parked in the garage.";
                string pattern = "[^c]ar";
                var matcher = new Regex(pattern);

                //Act
                var result = matcher.Matches(text);

                //Assert
                result.Count.Should().Be(2);
                result[0].Value.Should().Be("par");
                result[1].Value.Should().Be("gar");
            }

            /// <summary>
            /// The symbol * matches zero or more repetitions of the preceding matcher. 
            /// The regular expression a* means: zero or more repetitions of preceding lowercase character a. 
            /// But if it appears after a character set or class then it finds the repetitions of the 
            /// whole character set. 
            /// For example, the regular expression [a-z]* means: any number of lowercase letters in a row.
            /// </summary>
            [TestMethod]
            public void Repetitions_TheStar_ShouldMatchZeroToManyLowercaseLetters()
            {
                //Arrange
                var text = "The car parked in the garage";
                var pattern = "[a-z]*";
                var matcher = new Regex(pattern);

                //Act
                var result = Regex.Matches(text, pattern)
                                    .Cast<Match>()
                                    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                                    .ToArray();

                //Assert
                result.Count().Should().Be(6);
                result[0].Value.Should().Be("he");
                result[1].Value.Should().Be("car");
                result[2].Value.Should().Be("parked");
                result[3].Value.Should().Be("in");
                result[4].Value.Should().Be("the");
                result[5].Value.Should().Be("garage");

            }

            /// <summary>
            /// The * symbol can be used with the meta character . to match any string of characters .*. 
            /// The * symbol can be used with the whitespace character \s to match a string of whitespace characters. 
            /// For example, the expression \s*cat\s* means: zero or more spaces, 
            /// followed by lowercase character c, followed by lowercase character a, 
            /// followed by lowercase character t, followed by zero or more spaces.
            /// </summary>
            [TestMethod]
            public void Repetition_TheStarAndFullStop_ShouldMatchWordCatWithZeroToManySpacesInFrontAndAfter()
            {
                //Arrange
                var text = "The fat cat sat on the concatenation";
                var pattern = @"\s*cat\s*";

                //Act
                var result = Regex.Matches(text, pattern);

                //Assert
                result.Count.Should().Be(2);
                result[0].Value.Should().Be(" cat ");
                result[1].Value.Should().Be("cat");

            }

            /// <summary>
            /// The symbol + matches one or more repetitions of the preceding character. 
            /// For example, the regular expression c.+t means: lowercase letter c, 
            /// followed by at least one character, followed by the lowercase character t. 
            /// It needs to be clarified that t is the last t in the sentence
            /// </summary>
            [TestMethod]
            public void Repetition_ThePlus_ShouldMatchTheLastCharacterOfTheSentence()
            {
                //Arrange
                var text = "The fat car is parked on the mat while a mechanic works";
                var pattern = @"c.+t";

                //Act
                var result = Regex.Matches(text, pattern);

                //Assert
                result.Count.Should().Be(6);


            }



        }
    }
}
