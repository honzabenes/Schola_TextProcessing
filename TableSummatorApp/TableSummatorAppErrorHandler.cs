using FileProcessingConsoleAppFramework;

namespace TableSummatorApp
{
    internal class TableSummatorAppErrorHandler(TextWriter errorOutput) : AppErrorHandler(errorOutput)
    {
        public new void Execute(IProgramCore program, string[] args)
        {
            try
            {
                base.Execute(program, args);
            }
            catch (InvalidTableFormatException)
            {
                ErrorOutput.WriteLine("Invalid File Format");
            }
            catch (InvalidParseException)
            {
                ErrorOutput.WriteLine("Invalid Integer Value");
            }
            catch (NonExistenColumnNameException)
            {
                ErrorOutput.WriteLine("Non-existent Column Name");
            }
        }
    }
}
