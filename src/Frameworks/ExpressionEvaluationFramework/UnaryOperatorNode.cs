namespace ExpressionEvaluationFramework
{
    public abstract class UnaryOperatorNode : OperatorNode
    {
        public ExpressionTreeNode Operand { get; init; }

        public UnaryOperatorNode(ExpressionTreeNode operand)
        {
            Operand = operand;
        }
    }
}
