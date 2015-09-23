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
        public void TestFilePath1()
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
        public void TestFilePathNegative1()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { "filethatdoesnotexist.txt", "AND", "Care" });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(FileNotFoundException));
            }
        }

        [TestMethod]
        public void TestMatchTypeParse1()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "AND", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchTypeParse2()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "and", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchTypeParse3()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "anD", "Care" });
            Assert.AreEqual(MatchType.And, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchTypeParse4()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "or", "Care" });
            Assert.AreEqual(MatchType.Or, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchTypeParse5()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "not", "Care" });
            Assert.AreEqual(MatchType.Not, userInput.MatchType);
        }

        [TestMethod]
        public void TestMatchTypeParseNegative1()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "aaa", "Care" });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestSearchTermParse1()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "not", "Care" });
            Assert.AreEqual("Care", string.Join(",", userInput.Terms));
        }

        [TestMethod]
        public void TestSearchTermParseNegative1()
        {
            try
            {
                UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "not" });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentException));
            }
        }

        [TestMethod]
        public void TestSearchTermParse2()
        {
            UserInput userInput = new UserInput(new string[] { POSITIVE_TEST_NEWS_FILE_PATH, "not", "Care", "Quality", "Commission" });
            Assert.AreEqual("Care,Quality,Commission", string.Join(",", userInput.Terms));
        }
    }
}
