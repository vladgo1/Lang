namespace lang.ast
{
    public class UnaryExpression:IExpression
    {
        private string symbol;
        private IExpression expression;
        public UnaryExpression(string symbol, IExpression expression)
        {
            this.symbol = symbol;
            this.expression = expression;
        }

        public double Get()
        {
            if(symbol == "-")
                return -expression.Get();
                
            return expression.Get();
        }
    }
}