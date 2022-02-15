namespace lang.ast
{
    public class NumberExpression:IExpression
    {
        private double value;
        public NumberExpression(double value)
        {
            this.value = value;
        }
        
        public double Get()
        {
            return value;
        }

        public override string ToString()
        {
            return $"(value = {value})";
        }
    }
}