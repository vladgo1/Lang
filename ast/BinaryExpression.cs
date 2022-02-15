namespace lang.ast
{
    public class BinaryExpression:IExpression
    {
        private string symbol;
        private IExpression leftExpression;
        private IExpression rigthExpression;
        public BinaryExpression(string symbol, IExpression leftExpression, IExpression rigthExpression)
        {
            this.symbol = symbol;
            this.leftExpression = leftExpression;
            this.rigthExpression = rigthExpression;
        }

        public double Get()
        {
            if(symbol == "-")
                return leftExpression.Get() - rigthExpression.Get();
            if(symbol == "*")
                return leftExpression.Get() * rigthExpression.Get();
            if(symbol == "/")
            {
                if(rigthExpression.Get() == 0)
                    throw new Exception("division by zero");
                return leftExpression.Get() / rigthExpression.Get();    
            }
            return leftExpression.Get() + rigthExpression.Get();
        }

        public override string ToString()
        {
            return $"exp([{leftExpression} {symbol} {rigthExpression}])";
        }
    }
}