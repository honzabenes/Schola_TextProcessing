using ExcelFramework;

namespace Excel_UnitTests
{
    public class Sheet_UnitTests
    {
        [Fact]
        public void AddCell_EmptyCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new EmptyCell() }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void AddCell_NumberCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "6";

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new NumberCell(6) }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void AddCell_FormulaCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "=A2+D1";

            char @operator = '+';
            var op1address = new CellAddress(0, 1);
            var op2address = new CellAddress(3, 0);

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new FormulaCell(@operator, op1address, op2address) }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void GetCellValue_CellAddressNotInSheet() // The other cases are covered in Cell_UnitTests
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);

            // Act
            int value = sheet.GetCellValue(address);

            // Assert
            int expected = 0;

            Assert.Equal(expected, value);
        }


        [Fact]
        public void CalculateAll_NoFormulaCell()
        {
            // Arrange
            Dictionary<CellAddress, Cell> cells = new()
            {
                { new CellAddress(0, 0), new EmptyCell() },
                { new CellAddress(0, 1), new NumberCell(3) },
                { new CellAddress(1, 0), new NumberCell(15) },
                { new CellAddress(1, 1), new EmptyCell() }
            };

            var sheet = new Sheet(cells);

            // Act
            sheet.CalculateAll();

            List<int> calculatedValues = new();

            foreach (Cell cell in sheet.Cells.Values)
            {
                calculatedValues.Add((int)cell.Value!);
            }

            // Assert
            List<int> expected = new()
            {
                0, 3, 15, 0
            };

            Assert.Equal(expected, calculatedValues);
        }


        [Fact]
        public void CalculateAll_FormulaCellsNoCycles()
        {
            // Arrange
            Dictionary<CellAddress, Cell> cells = new()
            {
                { new CellAddress(0, 0), new EmptyCell() },
                { new CellAddress(0, 1), new NumberCell(3) },
                { new CellAddress(1, 0), new NumberCell(15) },
                { new CellAddress(1, 1), new EmptyCell() },
                { new CellAddress(0, 2), new FormulaCell('+', new CellAddress(0, 0), new CellAddress(0, 1)) },
                { new CellAddress(1, 2), new FormulaCell('*', new CellAddress(0, 1), new CellAddress(1, 0)) }
            };

            var sheet = new Sheet(cells);

            // Act
            sheet.CalculateAll();

            List<int> calculatedValues = new();

            foreach (Cell cell in sheet.Cells.Values)
            {
                calculatedValues.Add((int)cell.Value!);
            }

            // Assert
            List<int> expected = new()
            {
                0, 3, 15, 0, 3, 45
            };

            Assert.Equal(expected, calculatedValues);
        }


        [Fact]
        public void CalculateAll_FormulaCellLooksOutOfTheSheet()
        {
            // Arrange
            Dictionary<CellAddress, Cell> cells = new()
            {
                { new CellAddress(0, 0), new EmptyCell() },
                { new CellAddress(0, 1), new NumberCell(3) },
                { new CellAddress(1, 0), new NumberCell(15) },
                { new CellAddress(1, 1), new EmptyCell() },
                { new CellAddress(0, 2), new FormulaCell('+', new CellAddress(0, 0), new CellAddress(3, 4)) },
            };

            var sheet = new Sheet(cells);

            // Act
            sheet.CalculateAll();

            List<int> calculatedValues = new();

            foreach (Cell cell in sheet.Cells.Values)
            {
                calculatedValues.Add((int)cell.Value!);
            }

            // Assert
            List<int> expected = new()
            {
                0, 3, 15, 0, 0
            };

            Assert.Equal(expected, calculatedValues);
        }
    }
}
