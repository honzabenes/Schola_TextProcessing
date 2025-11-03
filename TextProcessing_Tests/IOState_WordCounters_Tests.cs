namespace TextProcessing_Tests
{
    public class IOState_WordCounters_Tests
    {
        [Fact]
        public void NoArgumentOneExpected()
        {
            // Arrange
            string[] args = { };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderFromCLIArguments(args);

            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, output);
        }


        [Fact]
        public void TooManyArgumentsOneExpected()
        {
            // Arrange
            string[] args = { "input.txt", "secondArg" };
            var IOState = new InputOutputState();

            var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            IOState.InitializeReaderFromCLIArguments(args);

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