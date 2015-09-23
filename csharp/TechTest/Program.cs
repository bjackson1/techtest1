namespace TechTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        private const string helpText = "Usage:\n   TechTest.exe NewsFilePath SearchType SearchTerm1 SearchTerm2 ...\n\n"
            + "Where:\n"
            + "   NewsFilePath: File path of source news items"
            + "   SearchType(s): AND OR\n"
            + "   SearchTerm(s): Any number of individual words to search on\n\n"
            + "Example:\n"
            + "   TechTest.exe news.txt AND Care Quality Commission\n\n"
            + "Notes:\n"
            + "   - Search terms are case sensitive";

        public static void Main(string[] args)
        {
            try
            {
                UserInput userInput = new UserInput(args);
                NewsItem[] newsItems = null;
                int[] itemIndexes = null;

                try
                {
                    newsItems = NewsItem.LoadItemsFromFile(userInput.FilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured loading news articles: {0}", ex.Message);
                }

                if (newsItems != null)
                {
                    itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, userInput.Terms, userInput.MatchType);
                }

                if (itemIndexes != null)
                {
                    Console.WriteLine("Found articles: {0}", string.Join(",", itemIndexes));
                }
                else
                {
                    Console.WriteLine("Unable to process articles.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n\n{1}", ex.Message, helpText);
            }
        }
    }
}
