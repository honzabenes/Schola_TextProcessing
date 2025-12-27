using System.Text;

namespace ExcelFramework
{
    public class SheetRenderer(TextWriter writer)
    {
        public void Render(Sheet sheet)
        {
            int maxRow = -1;

            List<CellAddress> addresses = sheet.Cells.Keys.ToList();

            foreach (CellAddress address in addresses)
            {
                if (address.Row > maxRow)
                {
                    maxRow = address.Row;
                }
            }

            if (maxRow == -1) return;

            int[] maxCols = new int[maxRow + 1];
            Array.Fill(maxCols, -1);

            foreach (CellAddress address in addresses)
            {
                if (address.Column > maxCols[address.Row])
                {
                    maxCols[address.Row] = address.Column;
                }
            }

            for (int r = 0; r <= maxRow; r++)
            {
                if (maxCols[r] == -1)
                {
                    writer.WriteLine();
                    continue;
                }

                var sb = new StringBuilder();

                for (int c = 0; c <= maxCols[r]; c++)
                {
                    CellAddress address = new CellAddress(c, r);

                    Cell? cell = sheet.GetCell(address);

                    if (cell is null)
                    {
                        sb.Append("[]");
                    }

                    else
                    {
                        sb.Append(cell.GetOutputString());
                    }

                    if (c <  maxCols[r])
                    {
                        sb.Append(' ');
                    }
                }

                writer.WriteLine(sb.ToString());
            }
        }
    }
}
