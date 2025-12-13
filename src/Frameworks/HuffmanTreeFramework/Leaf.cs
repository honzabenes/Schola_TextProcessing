using System.Text;

namespace ByteProcessingFramework
{
    public class Leaf : Node
    {
        public byte Value;

        public Leaf(byte value, int weight) 
            : base(0, weight, null, null)
        {
            Value = value;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"*{Value}:{Weight}");

            return sb.ToString();
        }
    }
}
