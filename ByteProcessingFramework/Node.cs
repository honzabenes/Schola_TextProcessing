using System.Text;

namespace ByteProcessingFramework
{
    public class Node : IComparable<Node>
    {
        public int CreatedAt { get; init; }
        public int Weight { get; init; }
        public Node? LeftChild;
        public Node? RightChild;

        public Node(int createdAt, int weigth, Node? leftChild, Node? rightChild)
        {
            CreatedAt = createdAt;
            Weight = weigth;
            LeftChild = leftChild;
            RightChild = rightChild;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{Weight}");

            return sb.ToString();
        }


        // I created comparing using AI: https://gemini.google.com/share/8bf36def65a6
        public int CompareTo(Node? other)
        {
            if (other is null) return 1;

            int weightComparison = this.Weight.CompareTo(other.Weight);
            if (weightComparison != 0)
            {
                return weightComparison;
            }

            return CompareEqualWeightNodes(this, other);
        }


        private int CompareEqualWeightNodes(Node a, Node b)
        {
            return (a, b) switch
            {
                // Both Nodes are Leafs, the value decides
                (Leaf l1, Leaf l2) => l1.Value.CompareTo(l2.Value),

                // One is Node, the other is Leaf, the Leaf has less weight
                (Leaf, Node) => -1,
                (Node, Leaf) => 1,

                // Both are Nodes, the older one has less weight
                (Node n1, Node n2) => n1.CreatedAt.CompareTo(n2.CreatedAt),
            };
        }
    }
}
