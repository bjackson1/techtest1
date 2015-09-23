namespace TechTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class NewsItemTest
    {
        private const string POSITIVE_TEST_NEWS_FILE_PATH = "PositiveTestNewsItems.txt";
        private const string NEGATIVE_TEST_NEWS_FILE_PATH = "NegativeTestNewsItems.txt";

        [TestMethod]
        public void TestLoadFromFile()
        {
            try
            {
                NewsItem[] newsItems = NewsItemTest.LoadNewsItems(NEGATIVE_TEST_NEWS_FILE_PATH);

                Assert.AreEqual(18, newsItems.Length);
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Failed to load news items from file: {0}", ex.Message));
            }
        }

        [TestMethod]
        public void TestLoadFromStringArray()
        {
            string[] rawItems = File.ReadAllLines(NEGATIVE_TEST_NEWS_FILE_PATH);

            try
            {
                NewsItem[] newsItems = NewsItem.LoadItems(rawItems);
                Assert.AreEqual(18, newsItems.Length);
            }
            catch (Exception ex)
            {
                Assert.Fail(string.Format("Failed to load news items from string[]: {0}", ex.Message));
            }
        }

        [TestMethod]
        public void TestSearchWithMultipleOrMatches1()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "Care", "Quality", "Commission" }, MatchType.Or);

            Assert.AreEqual("0,1,2,3,4,5,6", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestSearchWithMultipleOrMatches2()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "general", "population", "generally" }, MatchType.Or);

            Assert.AreEqual("6,8", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestSearchWithSingleOrMatches1()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "September", "2004" }, MatchType.Or);

            Assert.AreEqual("9", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestSearchWithSingleAndMatches1()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "Care", "Quality", "Commission", "admission" }, MatchType.And);

            Assert.AreEqual("1", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestSearchWithSingleAndMatches2()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "general", "population", "Alzheimer" }, MatchType.And);

            Assert.AreEqual("6", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestSearchWithNoAndMatches1()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "wibble" }, MatchType.And);

            Assert.AreEqual(0, itemIndexes.Length);
        }

        [TestMethod]
        public void TestSearchWithNotMatch()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "wibble123" }, MatchType.Not);

            Assert.AreEqual(0, itemIndexes.Length);
        }

        private static NewsItem[] LoadNewsItems(string filepath)
        {
            NewsItem[] newsItems = null;
            try
            {
                newsItems = NewsItem.LoadItemsFromFile(filepath);
            }
            catch (Exception ex)
            {
                Assert.Inconclusive(string.Format("Failed to load news items from file: {0}", ex.Message));
            }

            return newsItems;
        }
    }
}
