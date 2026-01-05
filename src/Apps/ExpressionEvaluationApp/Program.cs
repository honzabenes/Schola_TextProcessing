using ExpressionEvaluationFramework;
using TokenProcessingFramework;

namespace ExpressionEvaluationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ITokenReader reader = new ByCharsTokenReader(Console.In);
                var treeBuilder = new PrefixExpressionTreeBuilder(reader);

                ExpressionTreeNode root = treeBuilder.Parse();
                int result = root.Evaluate();

                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                Console.WriteLine("Format Error");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Overflow Error");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Divide Error");
            }
        }
    }
}
