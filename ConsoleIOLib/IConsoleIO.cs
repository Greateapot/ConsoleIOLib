namespace ConsoleIOLib
{
    /// <summary>
    /// Console Input/Output Interface
    /// </summary>
    public interface IConsoleIO
    {
        public ConsoleKeyInfo ReadKey(bool intercept = false);
        public string? ReadLine();
        public void WriteLine(object? value);
        public void Write(object? value);
        public void Clear();
    }
}