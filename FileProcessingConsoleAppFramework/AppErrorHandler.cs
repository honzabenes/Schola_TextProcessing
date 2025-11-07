namespace FileProcessingConsoleAppFramework
{
    public class AppErrorHandler(TextWriter errorOutput)
    {
        private TextWriter _errorOutput { get; set; } = errorOutput;

        public void Execute(IProgramCore program, string[] args)
        {
            try
            {
                program.Run(args);
            }
            catch (ApplicationException appEx)
            {
                _errorOutput.WriteLine(appEx.Message);
            }
        }
    }
}
