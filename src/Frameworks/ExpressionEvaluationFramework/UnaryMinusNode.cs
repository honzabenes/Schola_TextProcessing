namespace ExpressionEvaluationFramework
{
    public class UnaryMinusNode : UnaryOperatorNode
    {
        public UnaryMinusNode(ExpressionTreeNode operand) : base(operand) { }


        public override int Evaluate()
        {
            return checked(-Operand.Evaluate());
        }
    }
}
