namespace ExpressionEvaluationFramework
{
    public sealed class ConstantNode : ExpressionTreeNode
    {
        public int Value { get; set; }

        public ConstantNode(int value)
        {
            Value = value;
        }


        public override int Evaluate()
        {
            return Value;
        }
    }
}
