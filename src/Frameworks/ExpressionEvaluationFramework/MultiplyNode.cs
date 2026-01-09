namespace ExpressionEvaluationFramework
{
    public class MultiplyNode : BinaryOperatorNode
    {
        public MultiplyNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) : base(leftOperand, rightOperand) { }


        public override int Evaluate()
        {
            return checked(LeftOperand.Evaluate() * RightOperand.Evaluate());
        }
    }
}
