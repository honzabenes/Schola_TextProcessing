namespace TextProcessing
{
    public class ParagraphWordCounter : ITokenProcessor
    {
        public List<int> ParagraphWordCounts { get; private set; } = new List<int>();
        private int CurrentParagraph { get; set; } = 0;


        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TypeToken.Word:
                    if (ParagraphWordCounts.Count <= CurrentParagraph)
                    {
                        ParagraphWordCounts.Add(1);
                    }
                    else
                    {
                        ParagraphWordCounts[CurrentParagraph]++;
                    }
                    break;

                case TypeToken.EoP:
                    CurrentParagraph++;
                    break;

                case TypeToken.EoL:
                    break;

                default: break;
            }
        }


        public void WriteOut(TextWriter writer)
        {
            foreach (int count in ParagraphWordCounts)
            {
                writer.WriteLine(count);
            }
        }
    }
}
