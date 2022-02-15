using System;
namespace lang.ast.words
{
    class Program 
    {
        static void Main() 
        {
            StreamReader streamReader = new("code.txt");
            var code = streamReader.ReadToEnd();

            //code = "((2.5 + 2)) * 3";
            Lexer lexer = new(code);

            var tokens = lexer.Tokenize();

            foreach(Token token in tokens)
            {
                Console.WriteLine(token.ToString());
            }

            var parser = new Parser(tokens);
            parser.InitProgram();

            //var expressions = parser.Parsing();

            /*foreach(IExpression expression in expressions)
            {
                Console.WriteLine(expression.ToString() + expression.Get());
            }*/
        }
    }
}
