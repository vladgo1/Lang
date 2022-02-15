namespace lang
{
    public class Lexer
    {
        private string[] code;
        private List<Token> tokens;
        private string operators = "+|-|*|/|(|)|:|,";

        public Lexer(string code)
        {
            this.code = code.Split(Environment.NewLine);
            tokens = new();
        }

        public List<Token> Tokenize()
        {
            foreach(string line in code)
            {
                var currentLine = line.Trim();
                while(currentLine != string.Empty)
                {
                    var word = GetWord(currentLine);
                    var type = GetType(word);

                    AddToken(word, type);
                    currentLine = currentLine.Substring(word.Length).Trim();
                }
                AddToken("", TokenType.EOL);
            }
            tokens.RemoveAt(tokens.Count - 1);
            return tokens;
        }

        private string GetWord(string line)
        {
            if(line[0] == '_')
                throw new Exception("unknown tokens" +  line[0]);


            if(operators.Contains(line[0]))
            {
                return line[0].ToString();
            }
            

            var word = string.Empty;
            var index = 0;
            if(char.IsLetter(line[index]))
            {
                while(index < line.Length)
                {
                    if((char.IsLetter(line[index]) || char.IsDigit(line[index]) || line[index] == '_') && line[index] != ' ' )
                        word += line[index];
                    else
                        break;

                    index++;
                }
            }
            else if(char.IsDigit(line[index]))
            {
                while(index < line.Length)
                {
                    if((char.IsDigit(line[index]) || line[index] == '.') && line[index] != ' ' )
                        word += line[index];
                    else
                        break;

                    index++;
                }
                
                word = word.Replace('.', ',');
            }
            
            return word;
        } 
        private TokenType GetType(string word)
        {
            if(word == ":")
                return TokenType.TYPEOF;
            if(word == ",")
                return TokenType.COMMA;
            if(word == "+")
                return TokenType.PLUS;
            if(word == "-")
                return TokenType.MINUS;
            if(word == "*")
                return TokenType.MULTILPY;
            if(word == "/")
                return TokenType.DIVIDE;
            if(word == "(")
                return TokenType.LPAR;
            if(word == ")")
                return TokenType.RPAR;

            if(char.IsLetter(word[0]))
            {
                if(word == "program")
                    return TokenType.PROGRAM;
                if(word == "var")
                    return TokenType.VAR;
                if(word == "int")
                    return TokenType.INT;
                if(word == "real")
                    return TokenType.REAL;
                if(word == "bool")
                    return TokenType.BOOL;
                if(word == "begin")
                    return TokenType.BEGIN;
                if(word == "end")
                    return TokenType.EOF;
                else
                    return TokenType.VARIABLE;
            }

            if(char.IsDigit(word[0]))
            {
                var isDigit = float.TryParse(word, out _);
                    
                if(isDigit)
                {
                    return TokenType.NUMBER;
                }
                else
                {
                    throw new Exception("invalid number" + word);
                }
            }

            
            throw new Exception("unknown token: " + word);
        }
        private void AddToken(string text, TokenType type) => tokens.Add(new Token(text, type)); 
    }
}