namespace GameTK;

public static class Debug
{
    /// <summary>
    /// Global flag to show/hide debug messages
    /// </summary>
    public static bool ShowDebug = true;
    
    /// <summary>
    /// Prints a debug message to the console
    /// </summary>
    /// <param name="line"></param>
    public static void Print(string line)
    {
        if (ShowDebug)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Prints a warning message to the console
    /// </summary>
    /// <param name="line"></param>
    public static void Warning(string line)
    {
        if (ShowDebug)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Prints an error message to the console and throws an Exception
    /// </summary>
    /// <param name="line"></param>
    /// <param name="throwError">Set to false if the error shouldn't throw an Exception</param>
    /// <exception cref="Exception"></exception>
    public static void Error(string line, bool throwError = true)
    {
        if (ShowDebug)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ResetColor();
        }
        if (throwError)
            throw new Exception(line);
    }

    /// <summary>
    /// Prints a success message to the console
    /// </summary>
    /// <param name="line"></param>
    public static void Success(string line)
    {
        if (ShowDebug)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}