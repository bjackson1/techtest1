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
        public void TestLoadNewsItems_WillLoadAllItemsFromFileWithIncompleteLinesAndMultipleCharacterSets_WhenCalled()
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
        public void TestLoadNewsItems_WillInstantiateAllNewsInstancesFromStringArrayWithIncompleteLinesAndMultipleCharacterSets_WhenCalled()
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
        public void TestSearch_WillReturnTrue_WhenCalledWithMatchingAndSearch()
        {
            NewsItem newsItem = new NewsItem("July 9 , 2013 : The HSCIC has extended the consultation period on a draft list of conditions to be included in a proposed ' Present on Admission flag ' . There are a number of conditions that , whilst preventable , can be acquired in hospitals and have an adverse effect on a patients morbidity and / or involve substantial financial cost to the hospital . Analysis of these conditions is currently difficult as it is not always known whether a condition has been acquired during the patients stay or was present at the time of admission to the hospital . If introduced a Present on admission flag could enable identification of conditions that were acquired by patients during their stay and those that existed prior to admission . This would enable better analysis of these conditions , helping to attribute the condition to the appropriate timeframe and in turn identify good practice . The Health and Social Care Information Centre ( HSCIC ) is working with key stakeholders , including the Academy of Medical Royal Colleges ( AoMRC ) , Royal College of Nursing ( RCN ) and Care Quality Commission ( CQC ) to define a candidate list of Present on Admission conditions and associated guidance . We are keen to hear from stakeholders , particularly clinicians and healthcare specialists . The consultation has now been extended until Sunday 28 July to enable as wide a range of stakeholders to respond as possible . ");
            bool found = newsItem.Search(new string[] { "Care", "Quality", "Commission" }, MatchType.And);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestSearch_WillReturnTrue_WhenCalledWithMatchingOrSearch()
        {
            NewsItem newsItem = new NewsItem("July 9 , 2013 : The HSCIC has extended the consultation period on a draft list of conditions to be included in a proposed ' Present on Admission flag ' . There are a number of conditions that , whilst preventable , can be acquired in hospitals and have an adverse effect on a patients morbidity and / or involve substantial financial cost to the hospital . Analysis of these conditions is currently difficult as it is not always known whether a condition has been acquired during the patients stay or was present at the time of admission to the hospital . If introduced a Present on admission flag could enable identification of conditions that were acquired by patients during their stay and those that existed prior to admission . This would enable better analysis of these conditions , helping to attribute the condition to the appropriate timeframe and in turn identify good practice . The Health and Social Care Information Centre ( HSCIC ) is working with key stakeholders , including the Academy of Medical Royal Colleges ( AoMRC ) , Royal College of Nursing ( RCN ) and Care Quality Commission ( CQC ) to define a candidate list of Present on Admission conditions and associated guidance . We are keen to hear from stakeholders , particularly clinicians and healthcare specialists . The consultation has now been extended until Sunday 28 July to enable as wide a range of stakeholders to respond as possible . ");
            bool found = newsItem.Search(new string[] { "list", "wibble", "foo" }, MatchType.Or);
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void TestSearch_WillReturnFalse_WhenCalledWithNotMatchingAndSearch()
        {
            NewsItem newsItem = new NewsItem("July 9 , 2013 : The HSCIC has extended the consultation period on a draft list of conditions to be included in a proposed ' Present on Admission flag ' . There are a number of conditions that , whilst preventable , can be acquired in hospitals and have an adverse effect on a patients morbidity and / or involve substantial financial cost to the hospital . Analysis of these conditions is currently difficult as it is not always known whether a condition has been acquired during the patients stay or was present at the time of admission to the hospital . If introduced a Present on admission flag could enable identification of conditions that were acquired by patients during their stay and those that existed prior to admission . This would enable better analysis of these conditions , helping to attribute the condition to the appropriate timeframe and in turn identify good practice . The Health and Social Care Information Centre ( HSCIC ) is working with key stakeholders , including the Academy of Medical Royal Colleges ( AoMRC ) , Royal College of Nursing ( RCN ) and Care Quality Commission ( CQC ) to define a candidate list of Present on Admission conditions and associated guidance . We are keen to hear from stakeholders , particularly clinicians and healthcare specialists . The consultation has now been extended until Sunday 28 July to enable as wide a range of stakeholders to respond as possible . ");
            bool found = newsItem.Search(new string[] { "Care", "Quality", "wibble" }, MatchType.And);
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void TestSearch_WillReturnFalse_WhenCalledWithNotMatchingorSearch()
        {
            NewsItem newsItem = new NewsItem("July 9 , 2013 : The HSCIC has extended the consultation period on a draft list of conditions to be included in a proposed ' Present on Admission flag ' . There are a number of conditions that , whilst preventable , can be acquired in hospitals and have an adverse effect on a patients morbidity and / or involve substantial financial cost to the hospital . Analysis of these conditions is currently difficult as it is not always known whether a condition has been acquired during the patients stay or was present at the time of admission to the hospital . If introduced a Present on admission flag could enable identification of conditions that were acquired by patients during their stay and those that existed prior to admission . This would enable better analysis of these conditions , helping to attribute the condition to the appropriate timeframe and in turn identify good practice . The Health and Social Care Information Centre ( HSCIC ) is working with key stakeholders , including the Academy of Medical Royal Colleges ( AoMRC ) , Royal College of Nursing ( RCN ) and Care Quality Commission ( CQC ) to define a candidate list of Present on Admission conditions and associated guidance . We are keen to hear from stakeholders , particularly clinicians and healthcare specialists . The consultation has now been extended until Sunday 28 July to enable as wide a range of stakeholders to respond as possible . ");
            bool found = newsItem.Search(new string[] { "elephant", "foo", "wibble" }, MatchType.And);
            Assert.IsFalse(found);
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindSevenMatches_WhenCalledWithMutipleOrTerms()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "Care", "Quality", "Commission" }, MatchType.Or);

            Assert.AreEqual("0,1,2,3,4,5,6", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindTwoMatches_WhenCalledWithMultipleOrTerms()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "general", "population", "generally" }, MatchType.Or);

            Assert.AreEqual("6,8", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindOneMatch_WhenCalledWithMultipleOrTerms()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "September", "2004" }, MatchType.Or);

            Assert.AreEqual("9", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindOneMatch_WhenCalledWithMultipleAndTerms1()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "Care", "Quality", "Commission", "admission" }, MatchType.And);

            Assert.AreEqual("1", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindOneMatch_WhenCalledWithMultipleAndTerms2()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "general", "population", "Alzheimer" }, MatchType.And);

            Assert.AreEqual("6", string.Join(",", itemIndexes));
        }

        [TestMethod]
        public void TestFindNewsArticlesContaining_WillFindNoMatches_WhenCalledWithSingleObscureAndTerm()
        {
            NewsItem[] newsItems = NewsItemTest.LoadNewsItems(POSITIVE_TEST_NEWS_FILE_PATH);

            int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, new string[] { "wibble" }, MatchType.And);

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
