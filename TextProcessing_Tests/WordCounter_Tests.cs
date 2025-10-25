namespace TextProcessing_Tests
{
    public class WordCounter_Tests
    {
        [Fact]
        public void NoWord()
        {
            // Arrange.
            char[] separators = { '\t', ' ', '\r', '\n' };
            string input = "ahoj jak se mas";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, separators);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("4", output);
        }
    }
}
