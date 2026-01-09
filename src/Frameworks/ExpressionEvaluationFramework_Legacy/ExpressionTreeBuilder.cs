namespace ExpressionEvaluationFramework_Legacy
{
    public static class ExpressionTreeBuilder
    {
        public static Node? Build(string expression)
        {
            if (expression == string.Empty)
            {
                return null;
            }

            var stack = new Stack<Node>();

            char[] operators = { '+', '-', '*', '/', '~' };
            string[] values = expression.Split();


            for (int i = values.Length - 1; i >= 0; i--)
            {
                try
                {
                    int num = int.Parse(values[i]);

                    stack.Push(new NumberNode(num));
                    continue;
                }
                catch (OverflowException)
                {
                    throw new FormatException();
                }
                catch (FormatException) { }


                if (char.TryParse(values[i], out char ch) && operators.Contains(ch))
                {
                    if (ch == '~')
                    {
                        if (stack.Count < 1)
                        {
                            throw new FormatException();
                        }

                        Node child = stack.Pop();

                        stack.Push(new OperatorNode('~', child, null));
                        continue;
                    }

                    if (stack.Count < 2)
                    {
                        throw new FormatException(); // There are less than 2 operands
                    }

                    Node leftChild = stack.Pop();
                    Node rightChild = stack.Pop();

                    stack.Push(new OperatorNode(ch, leftChild, rightChild));
                    continue;
                }

                throw new FormatException(); // The value is neither an operand nor an operator.
            }

            if (stack.Count != 1)
            {
                throw new FormatException(); // There is no single root
            }

            return stack.Pop(); // Return the root
        }
    }
}
