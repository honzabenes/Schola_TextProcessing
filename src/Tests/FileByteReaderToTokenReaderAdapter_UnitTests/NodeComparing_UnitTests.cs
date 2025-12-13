using HuffmanTreeFramework;

namespace HuffmanTree_UnitTests
{
    public class NodeComparing_UnitTests
    {
        [Fact]
        public void Nodes_DifferentWeights_SmallerWeightComesFirst()
        {
            // Arrange
            Node lighterNode = new Node(5, 10, null, null);
            Node heavierNode = new Node(1, 15, null, null);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void Nodes_EqualWeights_OlderComesFirst()
        {
            // Arrange
            Node lighterNode = new Node(1, 15, null, null);
            Node heavierNode = new Node(5, 15, null, null);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void Leafs_DifferentWeights_SmallerWeightComesFirst()
        {
            // Arrange
            Leaf lighterNode = new Leaf(97, 10);
            Leaf heavierNode = new Leaf(97, 15);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void Leafs_EqualWeights_SmallerValueComesFirst()
        {
            // Arrange
            Leaf lighterNode = new Leaf(97, 10);
            Leaf heavierNode = new Leaf(101, 10);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void LeafVSNode_LeafHeavier_NodeComesFirst()
        {
            // Arrange
            Node lighterNode = new Node(5, 10, null, null);
            Leaf heavierNode = new Leaf(97, 15);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void LeafVSNode_NodeHeavier_LeafComesFirst()
        {
            // Arrange
            Leaf lighterNode = new Leaf(97, 10);
            Node heavierNode = new Node(5, 15, null, null);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }


        [Fact]
        public void LeafVSNode_EqualWeights_LeafComesFirst()
        {
            // Arrange
            Leaf lighterNode = new Leaf(0, 10);
            Node heavierNode = new Node(5, 10, null, null);

            // Act
            int result = lighterNode.CompareTo(heavierNode);

            // Assert
            Assert.True(result < 0);
        }
    }
}
