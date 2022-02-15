namespace lang
{
    public class Token
    {
        public string text;
        public TokenType type;

        public Token(string text, TokenType type)
        {
            this.text = text;
            this.type = type;
        }
        
        public override string ToString()
        {
            return type + " " + text;
        }
    }
}