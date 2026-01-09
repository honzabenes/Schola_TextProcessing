using ExpressionEvaluationFramework;
using TokenProcessingFramework;

namespace ExpressionEvaluationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string expression = Console.ReadLine();

            try
            {
                //ExpressionTreeNode? root = ExpressionTreeBuilder_Legacy.Build(expression);
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
