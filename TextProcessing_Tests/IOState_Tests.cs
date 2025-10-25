namespace TextProcessing_Tests
{
    public class IOState_Tests
    {
        [Fact]
        public void NoArgumentOneExpected()
        {
            // Arrange
            string[] args = { };
            var IOState = new InputOutputState();

            // Act
            IOState.InitializeReaderFromCLIArguments(args);

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, Console.Out.ToString());
        }


        [Fact]
        public void TooManyArgumentsOneExpected()
        {
            string[] args = { "input.txt", "secondArg" };
            var IOState = new InputOutputState();

            IOState.InitializeReaderFromCLIArguments(args);

            Assert.Equal(InputOutputState.ArgumentErrorMessage, Console.Out.ToString());
        }

        [Fact]
        public void NoArgumentThreeExpected()
        {
            // Arrange
            string[] args = { };
            var IOState = new InputOutputState();

            // Act
            IOState.InitializeReaderWriterColumnNameFromCLIArguments(args);

            // Assert
            Assert.Equal(InputOutputState.ArgumentErrorMessage, Console.Out.ToString());
        }


        [Fact]
        public void TooFewArgumentsThreeExpected()
        {
            string[] args = { "input.txt", "output.txt" };
            var IOState = new InputOutputState();

            IOState.InitializeReaderWriterColumnNameFromCLIArguments(args);

            Assert.Equal(InputOutputState.ArgumentErrorMessage, Console.Out.ToString());
        }


        [Fact]
        public void TooManyArgumentsThreeExpected()
        {
            string[] args = { "input.txt", "output.txt", "column", "fourth" };
            var IOState = new InputOutputState();

            IOState.InitializeReaderWriterColumnNameFromCLIArguments(args);

            Assert.Equal(InputOutputState.ArgumentErrorMessage, Console.Out.ToString());
        }


        [Fact]
        public void InputFileNotFound()
        {
            string[] args = { "fakepath.txt" };
            var IOState = new InputOutputState();

            IOState.InitializeReaderFromCLIArguments(args);

            Assert.Equal(InputOutputState.FileErrorMessage, Console.Out.ToString());
        }
    }
}
