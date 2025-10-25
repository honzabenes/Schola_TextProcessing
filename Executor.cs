namespace TextProcessing
{
    public static class Executor
    {
        public static void ProcessAllWords(TokenReader reader, ITokenProcessor processor)
        {
            Token token = reader.ReadToken();

            while (token.Type != TypeToken.EoI)
            {
                processor.ProcessToken(token);
                token = reader.ReadToken();
            }

            processor.WriteOut();
        }
    }
}
