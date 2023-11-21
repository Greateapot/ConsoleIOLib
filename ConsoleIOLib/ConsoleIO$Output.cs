namespace ConsoleIOLib
{
    public partial class ConsoleIO
    {
        public static void WriteLineFormat(string format, params object?[] args) =>
            WriteLine(string.Format(format, args));
    }
}