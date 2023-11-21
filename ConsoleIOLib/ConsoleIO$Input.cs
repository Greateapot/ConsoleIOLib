using System.Reflection;

namespace ConsoleIOLib
{
    public static partial class ConsoleIO
    {
        /// <summary>
        /// Функция-верификатор ввода.
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="value">значения</param>
        /// <returns>Истина, если значение корректно, иначе ложь</returns>
        public delegate string? Verifier<T>(T value);

        /// <summary>
        /// Ввод "сырой" строки.
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="verifier">Функция-верификатор вводимого значения</param>
        public static string InputRaw(
            string? message = null,
            Verifier<string>? verifier = null
        )
        {
            verifier ??= DefaultInputRawVerifier;
            string? rawResult;
            bool isValid;
            do
            {
                if (message != null)
                    Write(message);
                rawResult = ReadLine();
                if (rawResult == null)
                {
                    isValid = false;
                    WriteLine("Неверный ввод.");
                }
                else
                {
                    var verifierResult = verifier(rawResult);
                    isValid = verifierResult == null;
                    if (!isValid) WriteLine(verifierResult);
                }
            } while (!isValid);
            return rawResult!;
        }

        /// <summary>
        /// Ввод значения заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип объекта, содержащего метод TryParse</typeparam>
        /// <param name="message">Сообщение</param>
        /// <param name="verifier">Функция-верификатор вводимого значения</param>
        /// <exception cref="Exception">Используйте InputRaw для ввода строк</exception>
        public static T Input<T>(
            string? message = null,
            Verifier<T>? verifier = null
        ) where T : new()
        {
            if (typeof(T).Name == "String") throw new Exception("Use InputRaw instead!");
            verifier ??= DefaultInputVerifier<T>;
            T? result;
            bool isValid;
            do
            {
                if (message != null)
                    Write(message);
                if (!TryParse(ReadLine(), out result))
                {
                    isValid = false;
                    WriteLine("Неверный ввод.");
                }
                else
                {
                    var verifierResult = verifier(result!);
                    isValid = verifierResult == null;
                    if (!isValid) WriteLine(verifierResult);
                }
            } while (!isValid);
            return result!;
        }

        /// <summary>
        /// Функция-верификатор ввода "сырой" строки по умолчанию. Проверяет, чтоб длина строки была > 0.
        /// </summary>
        /// <param name="value">"сырая" строка</param>
        /// <returns>Истина, если длина строки > 0, иначе ложь</returns>
        internal static string? DefaultInputRawVerifier(string value) => value.Length > 0 ? null : "Некорректный ввод.";

        /// <summary>
        /// Функция-верификатор ввода значения по умолчанию.
        /// </summary>
        /// <typeparam name="T">тип значения</typeparam>
        /// <param name="value">значение</param>
        /// <returns>Всегда возвращает null</returns>
        internal static string? DefaultInputVerifier<T>(T _) => null;

        /// <summary>
        /// Вызов метода TryParse заданного типа.
        /// </summary>
        /// <typeparam name="T">Тип объекта, содержащего метод TryParse</typeparam>
        /// <param name="methodInfo">Информация о методе</param>
        /// <param name="input">Обрабатываемая строка</param>
        /// <param name="result">Результат</param>
        /// <returns>true если обработано успешно, иначе false</returns>
        /// <exception cref="Exception">Информация о методе некорректна</exception>
        /// <exception cref="Exception">Тип не имеет метода TryParse</exception>
        internal static bool TryParse<T>(string? input, out T result) where T : new()
        {
            var type = typeof(T);
            var parameters = new object[] { input ?? "", new T() };

            var methodInfo = type.GetMethod(
                "TryParse",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new Type[] { typeof(string), type.MakeByRefType() },
                null
             ) ?? throw new Exception($"Type {type} doesn't have TryParse method!");

            var rawResult = methodInfo.Invoke(
                type,
                BindingFlags.Static | BindingFlags.Public,
                null,
                parameters,
                null
            ) ?? throw new Exception("Incorrect method info");

            result = (T)parameters[1];
            return (bool)rawResult;
        }
    }
}