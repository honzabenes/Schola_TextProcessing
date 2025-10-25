using System.Text;

namespace TextProcessing_Tests
{
    public class ParagraphWordCounter_Tests
    {
        [Fact]
        public void NoWord()
        {
            // Arrange.
            string input = """
                   
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("", output);
        }


        [Fact]
        public void OneWordNoNewLines()
        {
            // Arrange.
            string input = "Hello";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }


        [Fact]
        public void OneWordNewLinesBefore()
        {
            // Arrange.
            string input = """

                    Hello
                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }


        [Fact]
        public void OneWordNewLinesBeforeAndAfter()
        {
            // Arrange.
            string input = """

                    Hello


                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("1", output);
        }


        [Fact]
        public void MoreWordsNoNewLines()
        {
            // Arrange.
            string input = "Hello world.";

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            Assert.Equal("2", output);
        }


        [Fact]
        public void MoreWordsNewLinesBeforeBetweenAndAfter()
        {
            // Arrange.
            string input = """

                Hello world.

                    What a nice day.
                        

                This is a test.


                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("2");
            sb.AppendLine("4");
            sb.AppendLine("4");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }


        [Fact]
        public void ComplexInput()
        {
            // Arrange.
            string input = """
                
                Lorem ipsum dolor sit amet, consectetuer
                adipiscing elit. Duis bibendum, lectus ut viverra rhoncus,
                dolor nunc faucibus libero, eget facilisis enim ipsum id lacus. Phasellus rhoncus. 
                Nulla non arcu  lacinia neque faucibus fringilla. Maecenas ipsum velit,
                consectetuer eu lobortis ut, dictum at dui. Duis bibendum, 
                lectus ut viverra   rhoncus, dolor nunc faucibus libero, eget facilisis 
                enim ipsum id lacus. Proin mattis lacinia justo. Aenean vel massa quis 
                mauris vehicula          lacinia. Nemo enim ipsam voluptatem quia voluptas sit
                aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos 
                qui ratione voluptatem sequi nesciunt.

                Nullam at arcu a        est sollicitudin euismod. Etiam bibendum elit eget erat. 
                Fusce aliquam vestibulum      ipsum. Etiam commodo dui eget wisi. Neque porro
                quisquam est,           quia dolor sit amet, consectetur, 
                adipisci velit, sed quia non numquam eius modi tempora incidunt ut 
                labore et dolore magnam aliquam quaerat voluptatem. Nulla non lectus 
                sed nisl molestie malesuada. Phasellus et lorem id felis nonummy placerat.


                Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit,
                sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.
                Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium 
                doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore 
                veritatis et quasi architecto beatae vitae dicta sunt explicabo.


                """;

            var sw = new StringWriter();
            var sr = new StringReader(input);

            ITokenProcessor wordCounter = new ParagraphWordCounter(sw);
            TokenReader tReader = new TokenReaderByChars(sr, Constants.WHITE_SPACES);

            // Act
            Executor.ProcessAllWords(tReader, wordCounter);
            string? output = sw.ToString().Trim();

            // Assert
            var sb = new StringBuilder();
            sb.AppendLine("93");
            sb.AppendLine("62");
            sb.AppendLine("54");

            string expected = sb.ToString().Trim();
            Assert.Equal(expected, output);
        }
    }
}
