using System.Text;

namespace TextProcessing_Tests
{
    public class WordFrequencyCounter_Tests
    {
        [Fact]
        public void NoWord()
        {
            // Arrange.
            string input = "";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("", output);
        }


        [Fact]
        public void OneWordOneOccurence()
        {
            // Arrange.
            string input = "Hello";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            string expected = "Hello: 1";
            Assert.Equal(expected, output);
        }


        [Fact]
        public void OneWordMoreOccurences()
        {
            // Arrange.
            string input = """
                    Hello
                Hello
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            string expected = "Hello: 2";
            Assert.Equal(expected, output);
        }


        [Fact]
        public void MoreWordsEachOneOccurence()
        {
            // Arrange.
            string input = """
                    Hello World
                bye.
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("bye.: 1");
            sb.AppendLine("Hello: 1");
            sb.AppendLine("World: 1");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }


        [Fact]
        public void MoreWordsMoreOccurences()
        {
            // Arrange.
            string input = """
                    Hello World
                bye. Hello  again.

                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("again.: 1");
            sb.AppendLine("bye.: 1");
            sb.AppendLine("Hello: 2");
            sb.AppendLine("World: 1");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }


        [Fact]
        public void MoreSameWordsWithDifferentCapitalizing()
        {
            // Arrange.
            string input = """
                    Hello   hello
                hEllo
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("hello: 1");
            sb.AppendLine("hEllo: 1");
            sb.AppendLine("Hello: 1");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }


        [Fact]
        public void MoreSameWordsSomeWithNonAlphabetChar()
        {
            // Arrange.
            string input = """
                    Hello   Hello.
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new WordFrequencyCounter();
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("Hello: 1");
            sb.AppendLine("Hello.: 1");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }
    }
}
