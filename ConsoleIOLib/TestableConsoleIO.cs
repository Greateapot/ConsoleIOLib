namespace ConsoleIOLib
{
    public class TestableConsoleIO : IConsoleIO
    {
        public TestableConsoleIO() { ConsoleIO.Instance = this; }

        private readonly Queue<object> input = new();

        public string? Output { get; private set; }

        public delegate void Callback(string? output);

        public void PushKey(params ConsoleKey[] keys)
        {
            foreach (var key in keys)
                input.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));
        }

        public void PushKeyInfo(params ConsoleKeyInfo[] keysInfo)
        {
            foreach (var keyInfo in keysInfo)
                input.Enqueue(keyInfo);
        }

        public void PushLine(params string[] lines)
        {
            foreach (var line in lines)
                input.Enqueue(line);
        }

        public void PushTest(params Callback[] callbacks)
        {
            foreach (var callback in callbacks)
                input.Enqueue(callback);
        }

        public void Clear() => Output = "";

        public ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            try
            {
                var key = input.Dequeue();
                switch (key)
                {
                    case Callback callback:
                        callback(Output);
                        return ReadKey(intercept);
                    case ConsoleKeyInfo consoleKeyInfo:
                        return consoleKeyInfo;
                    default:
                        throw new Exception($"Unexpected key input: {key}");
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Not enough inputs");
            }
        }

        public string? ReadLine()
        {
            try
            {
                var key = input.Dequeue();
                switch (key)
                {
                    case Callback callback:
                        callback(Output);
                        return ReadLine();
                    case string line:
                        return line;
                    default:
                        throw new Exception($"Unexpected line input: {key}");
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Not enough inputs");
            }
        }

        public void Write(object? value) => Output += value?.ToString() ?? "";

        public void WriteLine(object? value) => Output += (value?.ToString() ?? "") + "\n";
    }
}