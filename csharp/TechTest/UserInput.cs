namespace TechTest
{
    using System;
    using System.IO;
    using System.Linq;

    public class UserInput
    {
        public UserInput(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("Insufficient arguments specified");
            }
            else
            {
                this.FilePath = args[0];
                if (!File.Exists(this.FilePath))
                {
                    throw new FileNotFoundException(string.Format("The file {0} does not exist", this.FilePath));
                }

                MatchType matchType;
                if (Enum.TryParse<MatchType>(args[1], true, out matchType))
                {
                    this.MatchType = matchType;
                }
                else
                {
                    throw new ArgumentException("Invalid match type specified");
                }

                this.Terms = args.Skip(2).ToArray();
            }
        }

        public string FilePath
        {
            private set;
            get;
        }

        public MatchType MatchType
        {
            private set;
            get;
        }

        public string[] Terms
        {
            private set;
            get;
        }
    }
}
