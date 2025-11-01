namespace TextProcessing
{
    public class TextPrinter : ITokenProcessor
    {
        private TextWriter _writer;

        public TextPrinter(TextWriter writer)
        {
            _writer = writer;
        }


        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TypeToken.Word:
                    _writer.Write(token.Word);
                    break;

                case TypeToken.Space:
                    _writer.Write(" ");
                    break;

                case TypeToken.EoL:
                    _writer.Write('\n');
                    break;

                case TypeToken.EoP:
                    _writer.Write("\n\n");
                    break;

                case TypeToken.EoI:
                    _writer.Write("\n");
                    break;

                default:
                    break;
            }
        }

        public void WriteOut(TextWriter writer)
        {
            _writer.WriteLine("\nNot implemented WriteOut!!!");
            return;
        }
    }
}
