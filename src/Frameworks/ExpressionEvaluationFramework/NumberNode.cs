namespace ExpressionEvaluationFramework
{
    public class NumberNode : ExpressionTreeNode
    {
        public int Value { get; set; }

        public NumberNode(int value)
        {
            Value = value;
        }


        public override int Evaluate()
        {
            return Value;
        }
    }
}
