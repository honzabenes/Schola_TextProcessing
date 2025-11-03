namespace TextProcessing_Tests
{
    public class IOState_TableSummator_Tests
    {
        [Fact]
        public void NoArgumentThreeExpected()
        {
            // Arrange
            string[] args = { };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndColumnNameFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void TooFewArgumentsThreeExpected()
        {
            // Arrange
            string[] args = { "input.txt", "output.txt" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndColumnNameFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void TooManyArgumentsThreeExpected()
        {
            // Arrange
            string[] args = { "input.txt", "output.txt", "column", "fourth" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndColumnNameFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void InputFileNotFound()
        {
            // Arrange
            string[] args = { "fakepath.txt", "output.txt", "Price" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndColumnNameFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.FileErrorMessage, output);
        }
    }
}

