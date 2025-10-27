namespace TextProcessing_Tests
{
    public class WordCounter_Tests
    {
        [Fact]
        public void NoWord()
        {
            // Arrange.
            string input = """
                     
                   
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("0", output);
        }


        [Fact]
        public void OneWordNoWhiteSpaces()
        {
            // Arrange.
            string input = "hello";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }



        [Fact]
        public void OneWordSomeWhiteSpaces()
        {
            // Arrange.
            string input = """
                    
                  Hello.
                    
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }



        [Fact]
        public void MoreWordSomeWhiteSpaces()
        {
            // Arrange.
            string input = """
                    
                  Hello.    World

                What a  nice day.
                    
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("6", output);
        }
    }
}
