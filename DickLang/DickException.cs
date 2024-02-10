using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Pastel;

namespace DickLang
{
    public class DickException : Exception
    {
        public DickException() { }

        public DickException(string message) : base(message)
        {
            Console.WriteLine(message.Pastel(Color.Red));
        }

        public DickException(string message, Exception innerException) : base(message, innerException)
        {
            Console.WriteLine(message.Pastel(Color.Red));
        }
    }

    public static class ColoredConsole
    {
        public static void WriteLine(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Write(string message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteBackspaces(string count)
        {
            for (int i = 0; i < count.Length - 1; i++)
            {
                Console.Write("\b");
            }
        }
    }

    public static class DickExceptionDo
    {
        public static void Out(int line = -1, int charPositionInLine = -1, string offendingSymbol = null, string msg = "", bool exit = true)
        {
            //foreach (var v in DickGet.Variables)
            //{ Console.WriteLine($"{v.Key}: {v.Value}".Pastel(Color.Aqua)); }

            string fileName = Program.fileName;
            string lineText;

            if (charPositionInLine == -1 && offendingSymbol != null)
            {
                charPositionInLine = DickGet.CurrentLineText.IndexOf(offendingSymbol);
            }

            var lines = File.ReadAllLines(fileName);
            if (line != -1)
            {
                lineText = lines[line - 1];
            }
            else
            {
                int exLineCount = 0;
                using (StreamReader reader = new StreamReader(Program.fileName))
                {
                    string fileLine;
                    while ((fileLine = reader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(fileLine.TrimStart(), @"^//"))
                        {
                            exLineCount++;
                        }
                        else if (string.IsNullOrWhiteSpace(fileLine))
                        {
                            exLineCount++;
                        }
                    }
                }
                lineText = DickGet.CurrentLineText;
                line = DickGet.CurrentLineCount + exLineCount;
            }

            ColoredConsole.Write($"\nYOUR DICK HAS ERROR: ", ConsoleColor.Red);
            ColoredConsole.WriteLine(msg);
            ColoredConsole.Write("At fucking file-> ", ConsoleColor.Blue);
            ColoredConsole.WriteLine($"{fileName}");

            if (offendingSymbol != null)
            {
                ColoredConsole.Write("At fucking pos -> ", ConsoleColor.Blue);
                ColoredConsole.WriteLine($"{line}:{charPositionInLine}");

                Console.Write("    | \n    ");
                ColoredConsole.WriteBackspaces(line.ToString());

                Console.Write($"{line} ");


                Console.WriteLine(lineText);

                Console.Write("    | ");

                for (int i = 0; i < charPositionInLine; i++)
                {
                    Console.Write(" ");
                }
                for (int i = 0; i < offendingSymbol.Length; i++)
                {
                    ColoredConsole.Write("^", ConsoleColor.Red);
                }
            }

            ColoredConsole.WriteLine($" - {msg}", ConsoleColor.Red);

            if (exit)
                Environment.Exit(0);
        }
    }
}
