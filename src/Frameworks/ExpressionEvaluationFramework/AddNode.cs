namespace ExpressionEvaluationFramework
{
    public sealed class AddNode : BinaryOperatorNode
    {
        public AddNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) 
            : base(leftOperand, rightOperand) { }


        public override int Evaluate()
        {
            return checked(LeftOperand.Evaluate() + RightOperand.Evaluate());
        }
    }
}
