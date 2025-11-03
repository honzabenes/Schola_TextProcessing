namespace TextProcessing_Tests
{
    public class TextJustification_Tests
    {
        [Fact]
        public void emptyInput()
        {
            // Arrange
            string input = "";

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreatePipeline(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = "";

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void oneParagraph()
        {
            // Arrange
            string input = """
                Lorem ipsum dolor sit amet, meliore albucius torquatos pri cu. An mei aeterno detraxit definiebas, 
                eleifend scripserit ad nam. Cu quo graeci torquatos, 
                percipit salutandi no nam, 
                at mea natum nullam 
                omittam. Sed propriae corrumpit eu. Has recusabo 
                tincidunt inciderint ad, ne mea legere graeci.
                """;

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreatePipeline(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem   ipsum  dolor
                sit   amet,  meliore
                albucius   torquatos
                pri   cu.   An   mei
                aeterno     detraxit
                definiebas, eleifend
                scripserit  ad  nam.
                Cu     quo    graeci
                torquatos,  percipit
                salutandi no nam, at
                mea   natum   nullam
                omittam.         Sed
                propriae   corrumpit
                eu.   Has   recusabo
                tincidunt inciderint
                ad,  ne  mea  legere
                graeci.
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void multipleParagraphs()
        {
            // Arrange
            string input = """
                Lorem ipsum dolor sit amet, meliore albucius torquatos pri cu. An mei aeterno detraxit definiebas, 
                eleifend scripserit ad nam. Cu quo graeci torquatos, 
                percipit salutandi no nam, 
                at mea natum nullam 
                omittam. Sed propriae corrumpit eu. Has recusabo 
                tincidunt inciderint ad, ne mea legere graeci.

                    Primis iudicabit ut his,
                sit possim postulant an.
                            Eum no putent persius virtute,
                eius placerat vulputate ne vel.
                 Ea eum ponderum patrioque liberavisse.
                Movet viderer et mea, qui te insolens vituperata, duo habeo virtute no. Cu molestie adolescens est, eam mundi soleat appareat ad, an hinc graeci eum.
                """;

            int maxTextWidth = 30;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreatePipeline(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem  ipsum  dolor  sit amet,
                meliore albucius torquatos pri
                cu.  An  mei  aeterno detraxit
                definiebas,           eleifend
                scripserit   ad  nam.  Cu  quo
                graeci   torquatos,   percipit
                salutandi no nam, at mea natum
                nullam  omittam.  Sed propriae
                corrumpit   eu.  Has  recusabo
                tincidunt  inciderint  ad,  ne
                mea legere graeci.

                Primis  iudicabit  ut his, sit
                possim  postulant  an.  Eum no
                putent  persius  virtute, eius
                placerat  vulputate ne vel. Ea
                eum     ponderum     patrioque
                liberavisse.  Movet viderer et
                mea,     qui    te    insolens
                vituperata,  duo habeo virtute
                no.   Cu  molestie  adolescens
                est, eam mundi soleat appareat
                ad, an hinc graeci eum.
                """;

            // Assert
            Assert.Equal(expected, output);
        }
    }
}
