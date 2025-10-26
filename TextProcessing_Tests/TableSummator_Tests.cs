using System.Text;

namespace TextProcessing_Tests
{
    public class TableSummator_Tests
    {
        [Fact]
        public void FileIsEmpty()
        {
            // Arrange
            string input = "";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor tableSummator = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, tableSummator, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void NewLineInTable()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice

                Apple       5       20          4
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void NewLineBeforeTable()
        {
            // Arrange
            string input = """

                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void LastRowEndsWithNewLine()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4

                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("Price");
            sb.AppendLine("-----");
            sb.AppendLine("5");

            string expected = sb.ToString().Trim();

            Assert.Equal(expected, output);
        }


        [Fact]
        public void RowIsShorter()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          
                Pear        6       10          4 
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void LastRowIsShorter()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4
                Pear        6       10          
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void RowIsLonger()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4               12
                Pear        6       10          5
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid File Format", output);
        }


        [Fact]
        public void GivenColumnNameIsNotInTable()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Amount");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Non-existent Column Name", output);
        }


        [Fact]
        public void ValueInTableNotParsableByInt()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       Big          4
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Discount");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid Integer Value", output);
        }


        [Fact]
        public void ValueInTableOverflowsInt()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount        ActualPrice
                Apple       5       1000000000000   4
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Discount");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("Invalid Integer Value", output);
        }


        [Fact]
        public void CorrectInput()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount    ActualPrice
                Apple       5       20          4
                Pear        10      10          9
                Orange      16      25          12
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("Price");
            sb.AppendLine("-----");
            sb.AppendLine("31");

            string expected = sb.ToString().Trim();

            Assert.Equal(expected, output);
        }


        [Fact]
        public void CorrectInputWithMulitpleSameColumnTitles()
        {
            // Arrange
            string input = """
                Groceries   Price   Discount   ActualPrice   Price
                Apple       5       20         4             12
                Pear        10      10         9             80
                Orange      16      25         12            20
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new TableSummator(sw, "Price");
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter, sw);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("Price");
            sb.AppendLine("-----");
            sb.AppendLine("31");

            string expected = sb.ToString().Trim();

            Assert.Equal(expected, output);
        }
    }
}
