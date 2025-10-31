namespace TextProcessing
{
    public static class Executor
    {
        public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor, TextWriter writer, TextWriter errWriter)
        {
            try
            {
                Token token = reader.ReadToken();

                while (token.Type != TypeToken.EoI)
                {
                    processor.ProcessToken(token);
                    token = reader.ReadToken();
                }

                processor.ProcessToken(token);
            }
            catch (InvalidInputFormatException)
            {
                errWriter.WriteLine("Invalid File Format");
                return;
            }
            catch (NotParsableByIntException)
            {
                errWriter.WriteLine("Invalid Integer Value");
                return;
            }
            catch (NonExistenColumnNameInTableException)
            {
                errWriter.WriteLine("Non-existent Column Name");
                return;
            }

            processor.WriteOut(writer);
        }
    }
}
