namespace FileProcessingConsoleAppFramework
{
    /// <summary>
    /// Handler for executing the core program and catching application exceptions.
    /// </summary>
    public class AppErrorHandler(TextWriter errorOutput)
    {
        protected TextWriter ErrorOutput = errorOutput;


        public void Execute(IProgramCore program, string[] args)
        {
            try
            {
                program.Run(args);
            }
            catch (InvalidArgumentApplicationException)
            {
                ErrorOutput.WriteLine("Argument Error");
            }
            catch (FileAccesErrorApplicationException)
            {
                ErrorOutput.WriteLine("File Error");
            }
        }
    }
}
