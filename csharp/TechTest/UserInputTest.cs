namespace TechTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserInputTest
    {
        private const string POSITIVE_TEST_NEWS_FILE_PATH = "PositiveTestNewsItems.txt";
        private const string NEGATIVE_TEST_NEWS_FILE_PATH = "NegativeTestNewsItems.txt";

        [TestMethod]
        public void TestFilePath_WillSetFilePathProperty_WhenCalled()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "AND", "Care" });
                Assert.AreEqual(POSITIVE_TEST_NEWS_FILE_PATH, userInput.FilePath);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestFilePath_WillThrowFileNotFoundException_WhenCalledWithNonExistantFilePath()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { "filethatdoesnotexist.txt", "AND", "Care" });
                Assert.Fail("Expected {0} to be thrown.", typeof(FileNotFoundException));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(FileNotFoundException), ex.GetType());
            }
        }

        [TestMethod]
        public void TestMatchType_WillSetMatchTypeToAnd_WhenCalledWithAndParameterInUppercase()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "AND", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchType_WillSetMatchTypeToAnd_WhenCalledWithAndParameterInLowercase()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "and", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchType_WillSetMatchTypeToAnd_WhenCalledWithAndParameterInMixedCase()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "anD", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchType_WillSetMatchTypeToOr_WhenCalledWithOrParameter()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "or", "Care" });
            Assert.AreEqual(MatchType.Or, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchType_WillThrowArgumentException_WhenCalledWithInvalidMatchTypeParameter()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "aaa", "Care" });
                Assert.Fail("Expected {0} to be thrown.", typeof(ArgumentException));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestSearchTerm_WillSetSingleSearchTerm_WhenCalledWithSingleSearchTerm()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "and", "Care" });
            Assert.AreEqual("Care", string.Join(",", userInput.Terms));
        }

        [TestMethod]
        public void TestSearchTerm_WillTrowArgumentException_WhenCalledWithTwoArguments()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "and" });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestSearchTerm_WillSetThreeSearchTerms_WhenCalledWithFiveValidArguments()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "and", "Care", "Quality", "Commission" });
            Assert.AreEqual("Care,Quality,Commission", string.Join(",", userInput.Terms));
        }
    }
}
