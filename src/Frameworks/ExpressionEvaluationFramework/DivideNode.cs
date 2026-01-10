namespace ExpressionEvaluationFramework
{
    public sealed class DivideNode : BinaryOperatorNode
    {
        public DivideNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) 
            : base(leftOperand, rightOperand) { }

        public override int Evaluate()
        {
            return checked(LeftOperand.Evaluate() / RightOperand.Evaluate()); // can throw DivideByZeroException
        }
    }
}
