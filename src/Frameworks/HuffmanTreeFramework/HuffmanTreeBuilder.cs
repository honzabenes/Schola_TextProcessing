namespace ByteProcessingFramework
{
    public class HuffmanTreeBuilder
    {
        private List<Node> _nodes = new List<Node>();

        public HuffmanTreeBuilder(Dictionary<string, int> leafs)
        {
            foreach (KeyValuePair<string, int> item in leafs)
            {
                if (byte.TryParse(item.Key, out byte byteValue))
                {
                    _nodes.Add(new Leaf(byteValue, item.Value));
                }
            }
        }


        public Node Build()
        {
            int createdAt = 0;

            while (_nodes.Count > 1)
            {
                createdAt++;
                _nodes.Sort();

                Node leftNode = _nodes[0];
                Node rightNode = _nodes[1];

                _nodes.RemoveRange(0, 2);

                int parentWeight = leftNode.Weight + rightNode.Weight;

                Node parent = new Node(createdAt, parentWeight, leftNode, rightNode);

                _nodes.Add(parent);
            }

            Node root = _nodes[0];

            return root;
        }


        public void WriteOut(Node root)
        {
            if (root == null) return;

            Console.Write(root.ToString() + " ");
            WriteOut(root.LeftChild);
            WriteOut(root.RightChild);
        }
    }
}
