namespace ConsoleIOLib
{
    /// <summary>
    /// Default Console Input/Output (using <i>Console</i> class methods)
    /// </summary>
    public class DefaultConsoleIO : IConsoleIO
    {
        public void Clear() => Console.Clear();

        public ConsoleKeyInfo ReadKey(bool intercept = false) => Console.ReadKey(intercept);

        public string? ReadLine() => Console.ReadLine();

        public void Write(object? value) => Console.Write(value);

        public void WriteLine(object? value) => Console.WriteLine(value);
    }
}