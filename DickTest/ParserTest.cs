using Antlr4.Runtime;
using DickLang;

namespace DickTest
{
    [TestClass]
    public class ParserTest
    {
        private DickParser Setup(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            DickLexer dickLexer = new DickLexer(inputStream);
            CommonTokenStream commonTokenStream = new(dickLexer);
            DickParser dickParser =new DickParser(commonTokenStream);

            return dickParser;
        }
    }
}