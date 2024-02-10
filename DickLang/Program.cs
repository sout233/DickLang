using System.Drawing;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Tree;
using DickLang;
using Microsoft.VisualBasic;
using Pastel;
using static System.Net.Mime.MediaTypeNames;

public class Program
{
    public static string fileName = "Contents\\test.dick";
    public static bool isHasError = false;

    private static void Main(string[] args)
    {
        if(args.Length > 0)
        {
            fileName = args[0];
        }
        else
        {
            Console.WriteLine("NO DICK FILE INPUT, WILL USE DEFALUT PATH".Pastel(ConsoleColor.DarkYellow));
        }

        var fileContents = File.ReadAllText(fileName);

        AntlrInputStream inputStream = new(fileContents);
        DickLexer dickLexer = new(inputStream);
        CommonTokenStream commonTokenStream = new(dickLexer);
        DickParser dickParser = new(commonTokenStream);

        dickParser.RemoveErrorListeners();
        dickParser.AddErrorListener(DickErrorListener.Instance);

        var programContext = dickParser.program();
        DickGet dickGet = new();

        if (!isHasError)
        {
            DickGet.CurrentLineText = File.ReadAllLines(fileName)[programContext.Start.Line - 1];
            dickGet.Visit(programContext);
        }
        else
        {
            Console.WriteLine("\nYour DICK has error, PLS check it".Pastel(ConsoleColor.Red));
            Console.WriteLine("Exiting with code 0...");
            Environment.Exit(0);
        }
    }

    public class DickErrorListener : BaseErrorListener
    {
        public static readonly DickErrorListener Instance = new();

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            DickExceptionDo.Out(line, charPositionInLine, offendingSymbol.Text, msg, false);
            isHasError = true;
        }
    }
}