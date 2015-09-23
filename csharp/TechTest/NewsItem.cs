namespace TechTest
{
    using System.Collections.Generic;
    using System.IO;

    public class NewsItem
    {
        private string rawItem;

        public NewsItem(string rawItem)
        {
            this.rawItem = rawItem;
        }

        public bool Search(string[] terms, MatchType matchType)
        {
            int count = 0;

            foreach (string term in terms)
            {
                if (this.rawItem.Contains(term))
                {
                    count++;
                }
            }

            if (matchType.Equals(MatchType.And) && count.Equals(terms.Length)) return true;
            if (matchType.Equals(MatchType.Or) && ((count > 0) && (count <= terms.Length))) return true;
            return false;
        }

        public static NewsItem[] LoadItems(string[] items)
        {
            List<NewsItem> newsItems = new List<NewsItem>();

            foreach (string s in items)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    newsItems.Add(new NewsItem(s));
                }
            }

            return newsItems.ToArray();
        }

        public static NewsItem[] LoadItemsFromFile(string filepath)
        {
            return NewsItem.LoadItems(File.ReadAllLines(filepath));
        }

        public static int[] FindNewsArticlesContaining(NewsItem[] newsItems, string[] terms, MatchType searchOperator)
        {
            List<int> foundItemsIndexes = new List<int>();

            for(int i = 0; i < newsItems.Length; i++)
            {
                if (newsItems[i].Search(terms, searchOperator))
                {
                    foundItemsIndexes.Add(i);
                }
            }

            return foundItemsIndexes.ToArray();
        }
    }
}
