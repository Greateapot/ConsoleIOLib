namespace ConsoleIOLib
{
    /// <summary>
    /// Console Input/Output
    /// </summary>
    public static partial class ConsoleIO
    {
        private static IConsoleIO? instance;

        public static IConsoleIO Instance
        {
            get
            {
                return instance ??= new DefaultConsoleIO();
            }
            set
            {
                instance = value;
            }
        }

        public static ConsoleKeyInfo ReadKey(bool intercept = false) => Instance.ReadKey(intercept);
        public static string? ReadLine() => Instance.ReadLine();
        public static void WriteLine(object? value) => Instance.WriteLine(value);
        public static void Write(object? value) => Instance.Write(value);
        public static void Clear() => Instance.Clear();
    }
}