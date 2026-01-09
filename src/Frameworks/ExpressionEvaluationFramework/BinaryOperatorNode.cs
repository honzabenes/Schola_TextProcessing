namespace ExpressionEvaluationFramework
{
    public abstract class BinaryOperatorNode : OperatorNode
    {
        public ExpressionTreeNode LeftOperand { get; init; }
        public ExpressionTreeNode RightOperand { get; init; }

        public BinaryOperatorNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) 
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
        }
    }
}
