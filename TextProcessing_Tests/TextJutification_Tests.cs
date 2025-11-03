using System;

namespace TextProcessing_Tests
{
    public class TextJustification_Tests
    {
        [Fact]
        public void EmptyInput()
        {
            // Arrange
            string input = "";

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = "";

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void LongerWordThanMaxTextWidth()
        {
            // Arrange
            string input = """
                Loremipsumdolorsitamet
                ad nam. Cu qu
                """;

            int maxTextWidth = 10;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Loremipsumdolorsitamet
                ad nam. Cu
                qu
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void SingleWordLine()
        {
            // Arrange
            string input = """
                Loremnam
                ad nam. Cu qu
                """;

            int maxTextWidth = 10;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Loremnam
                ad nam. Cu
                qu
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void SameSpaceWidthsOnTheWholeLine()
        {
            // Arrange
            string input = """
                Lorem ipsum, meliore albucius torquatos p
                """;

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem ipsum, meliore
                albucius torquatos p
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void DifferentSpaceWidthsOnTheLine()
        {
            // Arrange
            string input ="""
                Lorem ipsum cu pri
                Lorem ipsum dolor si
                """;

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem  ipsum  cu pri
                Lorem ipsum dolor si
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void LastLineJustifiedToLeftWithNormalSpaces()
        {
            // Arrange
            string input = """
                Lorem ipsum dolor si
                Lorem ipsum
                """;

            int maxTextWidth = 20;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem ipsum dolor si
                Lorem ipsum
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void OneParagraph()
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

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

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
        public void MultipleParagraphs()
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

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

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


        [Fact]
        public void SingleLineInput()
        {
            // Arrange
            string input = """
                Has liber civibus ea. Ut his possim docendi, cum ne doming essent intellegam. Sea quodsi invidunt et, id vel nobis postea. Sit tantas tincidunt id, eos cu veniam labores adipisci, molestie suscipiantur ea nec. Tantas antiopam et has. Vis id ullum fabulas mnesarchum, vix ne lorem laudem convenire. Cu mel mutat soluta reformidans, cum ex odio animal, prima fierent mel no. His meis percipit molestiae at, natum officiis vix ea. Mel no salutatus aliquando. Mucius doming iriure no mel, fierent voluptatibus ut his. In eam esse nemore conclusionemque. Quod suavitate ei ius. No albucius suscipit vix, ei eros graece utroque vix, ne modus movet eloquentiam nam. Vix tibique tractatos dissentias te, aeque deseruisse mnesarchum mei an, dicat nonumes sed eu. Ut eirmod iisque splendide mea. Ex sumo principes referrentur qui. Id semper doctus nusquam mel. Brute everti sensibus ei eos, has liber impetus mediocritatem et, id sea scripta tractatos voluptatum. Ne illud novum principes cum. Cu pro possit vocent tamquam, vel te modo ferri volutpat. Vis ne eruditi appareat expetendis, labitur labores graecis in nam. Eum ullum impedit graecis in, labitur tractatos mea et, no cum inani putent fabulas. Impetus voluptua lobortis eu pri, sale erant in qui. Ei qui legendos mediocrem. Pri ex virtute pericula philosophia. Qui eu salutandi rationibus. Nibh zril eu nec. Ut ius aeque rationibus quaerendum, te vix solet putant impedit, ex reque persius delectus cum. Putant delenit comprehensam ex mea, ne per aliquid perfecto. Ne nec quando utinam liberavisse, sea ad mollis corrumpit. Virtute laoreet eu has, mel ne dicunt nostrud epicurei. Nam an vidisse oblique. Ex eos erat audire recteque.
                """;

            int maxTextWidth = 40;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Has  liber  civibus  ea.  Ut  his possim
                docendi,    cum    ne    doming   essent
                intellegam.  Sea  quodsi invidunt et, id
                vel  nobis  postea. Sit tantas tincidunt
                id,  eos  cu  veniam  labores  adipisci,
                molestie  suscipiantur  ea  nec.  Tantas
                antiopam  et  has.  Vis id ullum fabulas
                mnesarchum,    vix   ne   lorem   laudem
                convenire.    Cu    mel   mutat   soluta
                reformidans,  cum  ex odio animal, prima
                fierent   mel   no.  His  meis  percipit
                molestiae at, natum officiis vix ea. Mel
                no  salutatus  aliquando.  Mucius doming
                iriure  no  mel, fierent voluptatibus ut
                his. In eam esse nemore conclusionemque.
                Quod   suavitate  ei  ius.  No  albucius
                suscipit  vix,  ei  eros  graece utroque
                vix, ne modus movet eloquentiam nam. Vix
                tibique  tractatos  dissentias te, aeque
                deseruisse   mnesarchum  mei  an,  dicat
                nonumes   sed   eu.   Ut  eirmod  iisque
                splendide   mea.   Ex   sumo   principes
                referrentur   qui.   Id   semper  doctus
                nusquam  mel.  Brute  everti sensibus ei
                eos, has liber impetus mediocritatem et,
                id  sea scripta tractatos voluptatum. Ne
                illud novum principes cum. Cu pro possit
                vocent   tamquam,   vel  te  modo  ferri
                volutpat.   Vis   ne   eruditi  appareat
                expetendis,  labitur  labores graecis in
                nam.   Eum  ullum  impedit  graecis  in,
                labitur  tractatos  mea et, no cum inani
                putent    fabulas.    Impetus   voluptua
                lobortis  eu  pri, sale erant in qui. Ei
                qui  legendos  mediocrem. Pri ex virtute
                pericula  philosophia.  Qui eu salutandi
                rationibus.  Nibh  zril  eu  nec. Ut ius
                aeque   rationibus  quaerendum,  te  vix
                solet  putant  impedit, ex reque persius
                delectus     cum.     Putant     delenit
                comprehensam  ex  mea,  ne  per  aliquid
                perfecto.    Ne    nec   quando   utinam
                liberavisse,  sea  ad  mollis corrumpit.
                Virtute  laoreet  eu  has, mel ne dicunt
                nostrud   epicurei.   Nam   an   vidisse
                oblique. Ex eos erat audire recteque.
                """;

            // Assert
            Assert.Equal(expected, output);
        }


        [Fact]
        public void ComplexInput()
        {
            // Arrange
            string input = """
                Lorem ipsum dolor sit amet, meliore albucius torquatos pri cu. An mei aeterno detraxit definiebas, eleifend scripserit ad nam. Cu quo graeci torquatos, percipit salutandi no nam, at mea natum nullam omittam. Sed propriae corrumpit eu. Has recusabo tincidunt inciderint ad, ne mea legere graeci.

                Primis iudicabit ut his,
                sit possim postulant an.
                Eum no putent persius virtute,
                eius placerat vulputate ne vel.
                Ea eum ponderum patrioque liberavisse.
                Movet viderer et mea, qui te insolens vituperata, duo habeo virtute no. Cu molestie adolescens est, eam mundi soleat appareat ad, an hinc graeci eum.

                Has liber civibus ea. Ut his possim docendi, cum ne doming essent intellegam. Sea quodsi invidunt et, id vel nobis postea. Sit tantas tincidunt id, eos cu veniam labores adipisci, molestie suscipiantur ea nec. Tantas antiopam et has. Vis id ullum fabulas mnesarchum, vix ne lorem laudem convenire.

                Cu mel mutat soluta reformidans, cum ex odio animal, prima fierent mel no. His meis percipit molestiae at, natum officiis vix ea. Mel no salutatus aliquando. Mucius doming iriure no mel, fierent voluptatibus ut his. In eam esse nemore conclusionemque.

                Quod suavitate ei ius. No albucius suscipit vix, ei eros graece utroque vix, ne modus movet eloquentiam nam. Vix tibique tractatos dissentias te, aeque deseruisse mnesarchum mei an, dicat nonumes sed eu. Ut eirmod iisque splendide mea. Ex sumo principes referrentur qui.

                Id semper doctus nusquam mel. Brute everti sensibus ei eos, has liber impetus mediocritatem et, id sea scripta tractatos voluptatum. Ne illud novum principes cum. Cu pro possit vocent tamquam, vel te modo ferri volutpat. Vis ne eruditi appareat expetendis, labitur labores graecis in nam. Eum ullum impedit graecis in, labitur tractatos mea et, no cum inani putent fabulas.

                Impetus voluptua lobortis eu pri, sale erant in qui. Ei qui legendos mediocrem. Pri ex virtute pericula philosophia. Qui eu salutandi rationibus.

                Nibh zril eu nec. Ut ius aeque rationibus quaerendum, te vix solet putant impedit, ex reque persius delectus cum. Putant delenit comprehensam ex mea, ne per aliquid perfecto. Ne nec quando utinam liberavisse, sea ad mollis corrumpit. Virtute laoreet eu has, mel ne dicunt nostrud epicurei. Nam an vidisse oblique. Ex eos erat audire recteque.

                Sale albucius no sed, omnis soleat pertinacia eos in, duo an vidit iisque. Erant necessitatibus ea vis. Ex vix tollit dicunt, ius cu legere iriure concludaturque, usu omnesque voluptaria disputando at. Ad aliquid inermis detracto mei. Ad altera dissentiet qui, vix ad tota possit iriure.

                Albucius principes laboramus pri ei. Ei elit elitr eligendi mea. In ignota labores mea. Fuisset mentitum facilisis eam no, ad errem dicam accumsan sea. Nonumy tempor ceteros ad pro, nam aperiam epicuri voluptua ad.
                
                """;

            int maxTextWidth = 40;


            var sw = new StringWriter();
            Console.SetOut(sw);

            var sr = new StringReader(input);

            ITokenReader tokenReader = Program.CreateTokenReaderPipelineForLineJustifier(sr, maxTextWidth);

            var textPrinter = new TextPrinter(tokenReader, sw);


            // Act
            textPrinter.PrintAllTokens();

            string? output = sw.ToString().Trim();

            string expected = """
                Lorem  ipsum  dolor  sit  amet,  meliore
                albucius   torquatos   pri  cu.  An  mei
                aeterno  detraxit  definiebas,  eleifend
                scripserit   ad   nam.   Cu  quo  graeci
                torquatos, percipit salutandi no nam, at
                mea  natum  nullam omittam. Sed propriae
                corrumpit  eu.  Has  recusabo  tincidunt
                inciderint ad, ne mea legere graeci.

                Primis  iudicabit  ut  his,  sit  possim
                postulant  an.  Eum  no  putent  persius
                virtute, eius placerat vulputate ne vel.
                Ea  eum  ponderum patrioque liberavisse.
                Movet  viderer  et  mea, qui te insolens
                vituperata,  duo  habeo  virtute  no. Cu
                molestie   adolescens   est,  eam  mundi
                soleat appareat ad, an hinc graeci eum.

                Has  liber  civibus  ea.  Ut  his possim
                docendi,    cum    ne    doming   essent
                intellegam.  Sea  quodsi invidunt et, id
                vel  nobis  postea. Sit tantas tincidunt
                id,  eos  cu  veniam  labores  adipisci,
                molestie  suscipiantur  ea  nec.  Tantas
                antiopam  et  has.  Vis id ullum fabulas
                mnesarchum,    vix   ne   lorem   laudem
                convenire.

                Cu  mel mutat soluta reformidans, cum ex
                odio  animal,  prima fierent mel no. His
                meis   percipit   molestiae   at,  natum
                officiis   vix   ea.  Mel  no  salutatus
                aliquando.  Mucius doming iriure no mel,
                fierent voluptatibus ut his. In eam esse
                nemore conclusionemque.

                Quod   suavitate  ei  ius.  No  albucius
                suscipit  vix,  ei  eros  graece utroque
                vix, ne modus movet eloquentiam nam. Vix
                tibique  tractatos  dissentias te, aeque
                deseruisse   mnesarchum  mei  an,  dicat
                nonumes   sed   eu.   Ut  eirmod  iisque
                splendide   mea.   Ex   sumo   principes
                referrentur qui.

                Id  semper  doctus  nusquam  mel.  Brute
                everti   sensibus   ei  eos,  has  liber
                impetus mediocritatem et, id sea scripta
                tractatos  voluptatum.  Ne  illud  novum
                principes  cum.  Cu  pro  possit  vocent
                tamquam, vel te modo ferri volutpat. Vis
                ne  eruditi appareat expetendis, labitur
                labores   graecis   in  nam.  Eum  ullum
                impedit  graecis  in,  labitur tractatos
                mea et, no cum inani putent fabulas.

                Impetus  voluptua  lobortis eu pri, sale
                erant in qui. Ei qui legendos mediocrem.
                Pri ex virtute pericula philosophia. Qui
                eu salutandi rationibus.

                Nibh   zril   eu   nec.   Ut  ius  aeque
                rationibus   quaerendum,  te  vix  solet
                putant   impedit,   ex   reque   persius
                delectus     cum.     Putant     delenit
                comprehensam  ex  mea,  ne  per  aliquid
                perfecto.    Ne    nec   quando   utinam
                liberavisse,  sea  ad  mollis corrumpit.
                Virtute  laoreet  eu  has, mel ne dicunt
                nostrud   epicurei.   Nam   an   vidisse
                oblique. Ex eos erat audire recteque.

                Sale   albucius  no  sed,  omnis  soleat
                pertinacia  eos in, duo an vidit iisque.
                Erant  necessitatibus  ea  vis.  Ex  vix
                tollit  dicunt,  ius  cu  legere  iriure
                concludaturque,  usu omnesque voluptaria
                disputando   at.   Ad   aliquid  inermis
                detracto  mei. Ad altera dissentiet qui,
                vix ad tota possit iriure.

                Albucius  principes laboramus pri ei. Ei
                elit   elitr  eligendi  mea.  In  ignota
                labores  mea. Fuisset mentitum facilisis
                eam  no,  ad  errem  dicam accumsan sea.
                Nonumy   tempor   ceteros  ad  pro,  nam
                aperiam epicuri voluptua ad.
                """;

            // Assert
            Assert.Equal(expected, output);
        }
    }
}
