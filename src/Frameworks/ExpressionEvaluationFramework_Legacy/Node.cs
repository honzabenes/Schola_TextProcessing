namespace ExpressionEvaluationFramework_Legacy
{
    public abstract class Node
    {
        public Node? LeftChild { get; init; }
        public Node? RightChild { get; init; }

        public Node(Node? leftChild, Node? rightChild)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
        }
    }
}
