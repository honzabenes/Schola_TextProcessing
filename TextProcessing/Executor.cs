namespace TextProcessing
{
    public static class Executor
    {
        public static void ProcessAllWords(TokenReader reader, ITokenProcessor processor)
        {
            try
            {
                Token token = reader.ReadToken();

                while (token.Type != TypeToken.EoI)
                {
                    processor.ProcessToken(token);
                    token = reader.ReadToken();
                }
            }
            catch (InvalidTableFormatException)
            {
                Console.WriteLine("Invalid File Format");
            }

            processor.WriteOut();
        }
    }
}
