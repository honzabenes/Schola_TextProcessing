using TokenProcessingFramework;

namespace ExcelFramework
{
    public class SheetParser(Sheet sheet) : ITokenProcessor
    {
        int currentCol = 0;
        int currentRow = 0;


        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.EoL:
                    currentRow++;
                    currentCol = 0;
                    break;

                case TokenType.Word:
                    sheet.AddCell(new CellAddress(currentCol, currentRow), token.Word);
                    currentCol++;
                    break;

                default: break;
            }
        }


        public void WriteOut(TextWriter writer)
        {
            writer.WriteLine($"{currentRow}, {currentCol}");
        }
    }
}
