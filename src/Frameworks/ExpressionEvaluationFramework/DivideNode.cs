namespace ExpressionEvaluationFramework
{
    public class DivideNode : BinaryOperatorNode
    {
        public DivideNode(ExpressionTreeNode leftOperand, ExpressionTreeNode rightOperand) : base(leftOperand, rightOperand) { }

        public override int Evaluate()
        {
            int leftOperand = LeftOperand.Evaluate();
            int rightOperand = RightOperand.Evaluate();

            if (rightOperand == 0)
            {
                throw new DivideByZeroException();
            }

            return checked(leftOperand / rightOperand);
        }
    }
}
