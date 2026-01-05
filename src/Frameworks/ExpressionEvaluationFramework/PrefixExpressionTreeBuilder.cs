using TokenProcessingFramework;

namespace ExpressionEvaluationFramework
{
    public class PrefixExpressionTreeBuilder
    {
        private ITokenReader _reader;

        public PrefixExpressionTreeBuilder(ITokenReader reader)
        {
            _reader = reader;
        }


        public ExpressionTreeNode Parse()
        {
            ExpressionTreeNode root = ParseNextNode();
            Token token = _reader.ReadToken();

            if (token.Type != TokenType.EoL)
            {
                throw new FormatException();
            }

            return root;
        }


        public ExpressionTreeNode ParseNextNode()
        {

            Token token = _reader.ReadToken();

            if (token.Type == TokenType.Word)
            {
                string content = token.Word!;

                if (int.TryParse(content, out int num))
                {
                    return new NumberNode(num);
                }

                if (char.TryParse(content, out char ch))
                {
                    return ch switch
                    {
                        '~' => new UnaryMinusNode(ParseNextNode()),
                        '+' => new AddNode(ParseNextNode(), ParseNextNode()),
                        '-' => new SubstractNode(ParseNextNode(), ParseNextNode()),
                        '*' => new MultiplyNode(ParseNextNode(), ParseNextNode()),
                        '/' => new DivideNode(ParseNextNode(), ParseNextNode()),
                        _ => throw new FormatException()
                    };
                }
            }

            throw new FormatException();
        }
    }
}
