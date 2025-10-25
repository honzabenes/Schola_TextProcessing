namespace TextProcessing
{
    public class ParagraphWordCounter : ITokenProcessor
    {
        private TextWriter _writer;

        public List<int> ParagraphWordCounts { get; private set; } = new List<int>();
        private int CurrentParagraph { get; set; } = 0;

        public ParagraphWordCounter(TextWriter writer)
        {
            _writer = writer;
        }


        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TypeToken.Word:
                    if (ParagraphWordCounts.Count < CurrentParagraph)
                    {
                        ParagraphWordCounts.Add(1);
                    }
                    else
                    {
                        ParagraphWordCounts[CurrentParagraph - 1]++;
                    }
                    break;

                case TypeToken.EoP:
                    CurrentParagraph++;
                    break;

                case TypeToken.EoL:
                    break;
            }
        }


        public void WriteOut()
        {
            foreach (int count in ParagraphWordCounts)
            {
                _writer.WriteLine(count);
            }
        }
    }
}
