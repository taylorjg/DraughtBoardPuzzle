using System;

namespace DraughtBoardPuzzle
{
    public class ConsolePrintTarget : IPrintTarget
    {
        public void PrintLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
