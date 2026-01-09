namespace ExpressionEvaluationFramework_Legacy
{
    public static class ExpressionTreeEvaluator
    {
        public static int Evaluate(Node? root)
        {
            if (root is null)
            {
                return 0;
            }

            if (root is NumberNode numNode)
            {
                return numNode.Value;
            }

            if (root is OperatorNode opNode)
            {
                if (opNode.Operator == '~')
                {
                    int operand = Evaluate(opNode.LeftChild!);
                    return checked(-operand);
                }

                int leftOperand = Evaluate(opNode.LeftChild!);
                int rightOperand = Evaluate(opNode.RightChild!);

                if (opNode.Operator == '/' && rightOperand == 0)
                {
                    throw new DivideByZeroException();
                }

                int result = checked(opNode.Operator switch
                {
                    '+' => leftOperand + rightOperand,
                    '-' => leftOperand - rightOperand,
                    '*' => leftOperand * rightOperand,
                    '/' => leftOperand / rightOperand,
                    _ => 0 // Should be unreachable
                });

                return result;
            }

            return 0;
        }
    }
}
