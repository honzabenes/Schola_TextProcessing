namespace TokenProcessingFramework
{
    public static class TokenProcessing
    {
        public static void ProcessTokensUntilEndOfInput(ITokenReader reader, ITokenProcessor processor)
        {
            Token token;
            while ((token = reader.ReadToken()) is not { Type: TypeToken.EoI })
            {
                processor.ProcessToken(token);
            }

            processor.ProcessToken(token);
        }
    }
}
