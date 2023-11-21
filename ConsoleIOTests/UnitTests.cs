namespace ConsoleIOTests;

[CollectionDefinition("DNRIP", DisableParallelization = true)]
public class UnitTests
{
    [Fact]
    public void TestInputInt()
    {
        // arrange
        var consoleIO = new TestableConsoleIO();
        consoleIO.PushLine("invalidInput", "123");

        // act
        var result = ConsoleIO.Input<int>("Input int");

        // assert
        Assert.True(result == 123, "Значение должно быть равно 123");
    }

    [Fact]
    public void TestInputDouble()
    {
        // arrange
        var consoleIO = new TestableConsoleIO();
        consoleIO.PushLine("123,1231", "123,1241");

        // act
        var result = ConsoleIO.Input<double>(
            "Input double",
            verifier: (result) => result == 123.1231 ? "Некорректный ввод" : null
        );

        // assert
        Assert.True(result == 123.1241, "Значение должно быть равно 123.1241");
    }

    [Fact]
    public void TestInputRaw()
    {
        // arrange
        var consoleIO = new TestableConsoleIO();
        consoleIO.PushLine("", "123");

        // act
        var result = ConsoleIO.InputRaw("Input something");

        // assert
        Assert.True(result == "123", "Значение должно быть равно \"123\"");
    }

}