using System.Text;
using Exprese;

namespace ExpressionEvaluation_Tests
{
    public class ExpressionTreeBuilder_UnitTests
    {
        private void WriteOutExpressionTree(Node root, StringWriter sw)
        {
            if (root is NumberNode numNode)
            {
                sw.Write($"{numNode.Value} ");
            }

            if (root is OperatorNode opNode)
            {
                if (opNode.Operator == '~')
                {
                    sw.Write($"{opNode.Operator} ");
                    WriteOutExpressionTree(opNode.LeftChild!, sw);
                }
                else
                {
                    sw.Write($"{opNode.Operator} ");
                    WriteOutExpressionTree(opNode.LeftChild!, sw);
                    WriteOutExpressionTree(opNode.RightChild!, sw);
                }

            }
        }


        private const string FormatErrorMessage = "Format Error";
        private const string OverflowErrorMessage = "Overflow Error";

        
        [Fact]
        public void TwoNumbers()
        {
            // Arrange
            string expression = "3 ~ 5";
            var sw = new StringWriter();

            // Act
            try
            {
                Node root = ExpressionTreeBuilder.Build(expression);
                WriteOutExpressionTree(root, sw);
            }
            catch (OverflowException)
            {
                sw.WriteLine(OverflowErrorMessage);
            }
            catch (FormatException)
            {
                sw.WriteLine(FormatErrorMessage);
            }

            // Assert
            string expected = FormatErrorMessage;
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void OverflowInt_FormatError()
        {
            // Arrange
            string expression = "3000000000";
            var sw = new StringWriter();

            // Act
            try
            {
                Node root = ExpressionTreeBuilder.Build(expression);
                WriteOutExpressionTree(root, sw);
            }
            catch (OverflowException)
            {
                sw.WriteLine(OverflowErrorMessage);
            }
            catch (FormatException)
            {
                sw.WriteLine(FormatErrorMessage);
            }

            // Assert
            string expected = FormatErrorMessage;
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void InvalidToken()
        {
            // Arrange
            string expression = "a 3 2";
            var sw = new StringWriter();

            // Act
            try
            {
                Node root = ExpressionTreeBuilder.Build(expression);
                WriteOutExpressionTree(root, sw);
            }
            catch (OverflowException)
            {
                sw.WriteLine(OverflowErrorMessage);
            }
            catch (FormatException)
            {
                sw.WriteLine(FormatErrorMessage);
            }

            // Assert
            string expected = FormatErrorMessage;
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void InvalidOperandsCount()
        {
            // Arrange
            string expression = "+ 3";
            var sw = new StringWriter();

            // Act
            try
            {
                Node root = ExpressionTreeBuilder.Build(expression);
                WriteOutExpressionTree(root, sw);
            }
            catch (OverflowException)
            {
                sw.WriteLine(OverflowErrorMessage);
            }
            catch (FormatException)
            {
                sw.WriteLine(FormatErrorMessage);
            }

            // Assert
            string expected = "Format Error";
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void SingleNumber_CorrectInput()
        {
            // Arrange
            string expression = "3";
            var sw = new StringWriter();

            // Act
            Node root = ExpressionTreeBuilder.Build(expression);
            WriteOutExpressionTree(root, sw);

            // Assert
            string expected = "3";
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Complex_CorrectInput()
        {
            // Arrange
            string expression = "/ + - 5 2 * 2 + 3 3 ~ 2";
            var sw = new StringWriter();

            // Act
            Node root = ExpressionTreeBuilder.Build(expression);
            WriteOutExpressionTree(root, sw);

            // Assert
            string expected = "/ + - 5 2 * 2 + 3 3 ~ 2";
            string actual = sw.ToString().Trim();

            Assert.Equal(expected, actual);
        }
    }
}
