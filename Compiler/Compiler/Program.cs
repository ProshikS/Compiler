using System;

namespace Компилятор
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("test.txt\n");
            InputOutput.Init("test.txt");
            var analyzer = new LexicalAnalyzer();
            analyzer.Run();
        }
    }
}