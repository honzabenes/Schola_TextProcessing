namespace ExpressionEvaluationFramework
{
    public class SubstractNode : BinaryOperatorNode
    {
        public SubstractNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) : base(leftOperand, rightOperand) { }


        public override int Evaluate()
        {
            return checked(LeftOperand.Evaluate() - RightOperand.Evaluate());
        }
    }
}
