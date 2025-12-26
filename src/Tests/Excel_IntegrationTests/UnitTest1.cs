using System.Text;
using ExcelFramework;
using FileProcessingConsoleAppFramework;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using TokenProcessingFramework;

namespace Excel_IntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string input = """
                10 5 =A1+B1
                =A1-B1 =A1*B1 =A1/B1
                =C1+A2
                """;

            var sr = new StreamReader(input);
            var sw = new StringWriter();

            var tokenReader = new ByCharsTokenReader(sr);

            var sheet = new Sheet();
            var sheetParser = new SheetParser(sheet);
            var sheetRenderer = new SheetRenderer(sw);

            // Act
            TokenProcessing.ProcessTokensUntilEndOfInput(tokenReader, sheetParser);

            sheet.CalculateAll();

            sheetRenderer.Render(sheet);
            string? output = sw.ToString().Trim();

            // Assert
            string expected = """
                10 5 15
                5 50 2
                20
                """;

            //string expected = sb.ToString().Trim();

            Assert.Equal(expected, output);
        }
    }
}
