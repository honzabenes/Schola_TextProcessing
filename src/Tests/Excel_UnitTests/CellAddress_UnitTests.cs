using ExcelFramework;

namespace Excel_UnitTests
{
    public class CellAddress_UnitTests
    {
        [Fact]
        public void CellAddress_ctor_OneLetterOneNumber()
        {
            // Arrange
            var address = new CellAddress("A3");

            (int, int) coordinates = (address.ColIdx, address.RowIdx);

            // Assert
            (int, int) expected = (0, 2);

            Assert.Equal(expected, coordinates);
        }


        [Fact]
        public void CellAddress_ctor_MultipleLettersMultipleNumbers()
        {
            // Arrange
            var address = new CellAddress("AC321");

            (int, int) coordinates = (address.ColIdx, address.RowIdx);

            // Assert
            (int, int) expected = (28, 320);

            Assert.Equal(expected, coordinates);
        }
    }
}
