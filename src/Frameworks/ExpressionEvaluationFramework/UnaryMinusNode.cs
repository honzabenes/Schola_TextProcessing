namespace ExpressionEvaluationFramework
{
    public sealed class UnaryMinusNode : UnaryOperatorNode
    {
        public UnaryMinusNode(ExpressionTreeNode operand) : base(operand) { }


        public override int Evaluate()
        {
            return checked(-Operand.Evaluate());
        }
    }
}
