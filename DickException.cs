using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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

    public static class DickExceptionDo
    {
        public static void Out(int line = -1, int charPositionInLine = -1, string offendingSymbol = null, string msg = "", bool exit = true)
        {
            string fileName = Program.fileName;
            string lineText;

            if (charPositionInLine == -1)
            {
                charPositionInLine = DickGet.CurrentLine.IndexOf(offendingSymbol);
            }

            var lines = File.ReadAllLines(fileName);
            if (line != -1)
            {
                lineText = lines[line - 1];
            }
            else
            {
                lineText = DickGet.CurrentLine;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(offendingSymbol))
                    {
                        line = i + 1;
                        break;
                    }
                }
            }

            Console.WriteLine("\nYOUR DICK HAS ERROR: ".Pastel(Color.IndianRed) + msg.Pastel(Color.Azure));
            Console.WriteLine($"{"At Fucking Line".Pastel(Color.BlueViolet)}: {line}:({charPositionInLine})");
            Console.WriteLine($"{"At Fucking File".Pastel(Color.BlueViolet)}: {fileName}");

            Console.Write("\t");

            Console.WriteLine(lineText);

            Console.Write("\t");

            for (int i = 0; i < charPositionInLine; i++)
            {
                Console.Write(" ");
            }
            for (int i = 0; i <= offendingSymbol.Length; i++)
            {
                Console.Write("^".Pastel(Color.IndianRed));
            }

            Console.WriteLine($" - {msg}".Pastel(Color.IndianRed));

            if (exit)
                Environment.Exit(0);
        }
    }
}
