namespace ExpressionEvaluationFramework_Legacy
{
    public class OperatorNode : Node
    {
        public char Operator { get; init; }

        public OperatorNode(char @operator, Node leftChild, Node? rightChild)
            : base(leftChild, rightChild) 
        { 
            Operator = @operator; 
        }
    }
}
