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

            Token token = _reader.ReadToken();

            if (token.Type == TokenType.Word)
            {
                string content = token.Word!;

                if (int.TryParse(content, out int num))
                {
                    return new ConstantNode(num);
                }

                if (char.TryParse(content, out char ch))
                {
                    return ch switch
                    {
                        '~' => new UnaryMinusNode(Parse()),
                        '+' => new AddNode(Parse(), Parse()),
                        '-' => new SubstractNode(Parse(), Parse()),
                        '*' => new MultiplyNode(Parse(), Parse()),
                        '/' => new DivideNode(Parse(), Parse()),
                        _ => throw new NotSupportedException()
                    };
                }
            }

            throw new FormatException();
        }
    }
}