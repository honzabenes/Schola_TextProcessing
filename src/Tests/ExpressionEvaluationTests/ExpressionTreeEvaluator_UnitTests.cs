using Exprese;

namespace ExpressionEvaluation_Tests
{
    public class ExpressionTreeEvaluator_UnitTests
    {
        private const string DivideByZeroErrorMessage = "Divide Error";

        [Fact]
        public void DivideByZero()
        {
            // Arrange
            Node leftOperand = new NumberNode(5);
            Node rightOperand = new NumberNode(0);
            Node root = new OperatorNode('/', leftOperand, rightOperand);

            string result;

            // Act
            try
            {
                result = $"{ExpressionTreeEvaluator.Evaluate(root)}";
            }
            catch (DivideByZeroException)
            {
                result = DivideByZeroErrorMessage;
            }

            // Assert
            Assert.Equal(DivideByZeroErrorMessage, result);
        }


        [Fact]
        public void SingleNumber_CorrectInput()
        {
            // Arrange
            Node root = new NumberNode(5);

            // Act
            int result = ExpressionTreeEvaluator.Evaluate(root);

            // Assert
            Assert.Equal(5, result);
        }


        [Fact]
        public void SingleOperation_CorrectInput()
        {
            // Arrange
            Node leftOperand = new NumberNode(5);
            Node rightOperand = new NumberNode(3);
            Node root = new OperatorNode('+', leftOperand, rightOperand);

            // Act
            int result = ExpressionTreeEvaluator.Evaluate(root);

            // Assert
            Assert.Equal(8, result);
        }


        [Fact]
        public void NestedOperation_CorrectInput()
        {
            // Arrange
            Node leftOperand = new NumberNode(5);
            Node rightOperand = new NumberNode(3);
            Node nestedOp = new OperatorNode('+', leftOperand, rightOperand);
            Node root = new OperatorNode('*', nestedOp, new NumberNode(2));

            // Act
            int result = ExpressionTreeEvaluator.Evaluate(root);

            // Assert
            Assert.Equal(16, result);
        }
    }
}
