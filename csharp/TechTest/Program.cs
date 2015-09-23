namespace TechTest
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        private const string HELP_TEXT = "Usage:\n   TechTest.exe NewsFilePath SearchType SearchTerm1 SearchTerm2 ...\n\n"
            + "Where:\n"
            + "   NewsFilePath: File path of source news items\n"
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
                NewsItem[] newsItems = NewsItem.LoadItemsFromFile(userInput.FilePath);
                int[] itemIndexes = NewsItem.FindNewsArticlesContaining(newsItems, userInput.Terms, userInput.MatchType);
                Console.WriteLine("Found {0} articles: {1}", itemIndexes.Length, string.Join(",", itemIndexes));
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\n\n{1}", ex.Message, HELP_TEXT);
            }
        }
    }
}
