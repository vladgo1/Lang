namespace lang.ast.words
{
    public class Parser
    {
        private Token EOF = new("", TokenType.EOF);
        private int index = 0;
        private int size;
        private List<Token> tokens;
        private Variables variables;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            size = tokens.Count;
            variables = new();
        }

        public List<IExpression> Parsing()
        {
            List<IExpression> root = new();
            /*while(!Match(TokenType.EOF)) 
            {
                root.Add(Expression());
            }*/

            return root;
        }

        public void InitProgram()
        {   
            if(Match(TokenType.PROGRAM))
            {
                if(tokens[size - 1].type == TokenType.EOF)
                {
                    size--;
                    if(Match(TokenType.EOL))
                    {
                        InitVariables();
                    }
                    return;
                }
                throw new Exception("missing token: end");
            }
            throw new Exception("missing token: program");
        }

        private void InitVariables()
        {
            if(Match(TokenType.VAR))
            {
                //получение переменных
                while(true)
                {
                    var line = GetVar();
                //добавление в память
                if(Match(TokenType.INT))
                {
                    foreach(Token token in line)
                    {
                        if(!variables.IsExist(token.text))
                        {
                            variables.Add(token.text, 0);
                            continue;
                        }
                        throw new Exception("the variable already exists:"+ token.text);
                    }
                }
                else if(Match(TokenType.REAL))
                {
                    foreach(Token token in line)
                    {
                        if(!variables.IsExist(token.text))
                        {
                            variables.Add(token.text, 0.0f);
                            continue;
                        }
                        throw new Exception("the variable already exists:"+ token.text);
                    }
                }
                else if(Match(TokenType.BOOL))
                {
                    foreach(Token token in line)
                    {
                        if(!variables.IsExist(token.text))
                        {
                            variables.Add(token.text, false);
                            continue;
                        }
                        throw new Exception("the variable already exists:"+ token.text);
                    }
                }
                if(!Match(TokenType.EOL))
                    throw new Exception("expected line feed");
                

                if(Match(TokenType.BEGIN))
                    break;
                }
            }

            Console.WriteLine("переменные успешно добавлены");
        }

        private List<Token> GetVar()
        {
            var line = new List<Token>();
            while(Match(TokenType.VARIABLE))
            {
                line.Add(Peek(-1));
                if(Match(TokenType.COMMA))
                {
                    continue;
                }
                if(Match(TokenType.TYPEOF))
                {
                    break;
                }
                throw new Exception("Unexpected token:" + Peek(0));
            }

            return line;
        }

        private IExpression Expression() 
        {
            return Additive();
        }
    
        private IExpression Additive() 
        {
            IExpression result = Multiplicative();
        
            while(true)
            {
                if (Match(TokenType.PLUS)) 
                {
                    result = new BinaryExpression("+", result, Multiplicative());
                    continue;
                }
                if (Match(TokenType.MINUS)) 
                {
                    result = new BinaryExpression("-", result, Multiplicative());
                    continue;
                }
                break;
            }
        
            return result;
        }
    
        private IExpression Multiplicative() 
        {
            IExpression result = Unary();
        
            while(true) 
            {
                if (Match(TokenType.MULTILPY)) 
                {
                    result = new BinaryExpression("*", result, Unary());
                    continue;
                }
                if (Match(TokenType.DIVIDE)) 
                {
                    result = new BinaryExpression("/", result, Unary());
                    continue;
                }
                break;
            }   
        
            return result;
        }
    
        private IExpression Unary() 
        {
            if(Match(TokenType.MINUS)) 
            {
                return new UnaryExpression("-", primary());
            }
            if(Match(TokenType.PLUS)) 
            {
                return primary();
            }

            return primary();
        }
    
        private IExpression primary() 
        {
            Token current = Peek(0);
            if (Match(TokenType.NUMBER)) 
            {
                return new NumberExpression(double.Parse(current.text));
            }   
            if(Match(TokenType.LPAR)) 
            {
                IExpression result = Expression();
                Match(TokenType.RPAR);
                return result;
            }
            throw new Exception("Unknown expression");
        }
        
    

        private bool Match(TokenType type) 
        {
            Token current = Peek(0);
            if (type != current.type) return false;
            index++;
            return true;
        }
    
        private Token Peek(int relativePosition) 
        {
            int position = index + relativePosition;
            if (position >= size) 
                return EOF;

            return tokens[position];
        }
    }
}