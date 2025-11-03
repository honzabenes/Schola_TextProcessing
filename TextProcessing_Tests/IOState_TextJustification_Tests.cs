namespace TextProcessing_Tests
{
    public class IOState_TextJustitifcation_Tests
    {
        [Fact]
        public void NoArgument()
        {
            // Arrange
            string[] args = { };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndMaxTextWidthFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void TooFewArguments()
        {
            // Arrange
            string[] args = { "input.txt", "output.txt" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndMaxTextWidthFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void TooManyArguments()
        {
            // Arrange
            string[] args = { "input.txt", "output.txt", "30", "fourth" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderWriterAndMaxTextWidthFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void InputFileNotFound()
        {
            // Arrange
            string[] args = { "fakepath.txt" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.FileErrorMessage, output);
        }
    }
}
