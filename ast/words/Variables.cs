namespace lang.ast.words
{
    public class Variables
    {
        private Dictionary<string, int> INT;
        private Dictionary<string, float> REAL;
        private Dictionary<string, bool> BOOL;

        public Variables()
        {
            INT = new();
            REAL = new();
            BOOL = new();
        }
        public void Add(string name, int value)
        {
            if(!IsExist(name))
            {
                INT.Add(name, value);
                return;
            }
                
            throw new Exception("variable already exist:" + name);
        }

        public void Add(string name, float value)
        {
            if(!IsExist(name))
            {
                REAL.Add(name, value);
                return;
            }
                
            throw new Exception("variable already exist:" + name);
        }

        public void Add(string name, bool value)
        {
            if(!IsExist(name))
            {
                BOOL.Add(name, value);
                return;
            }
                
            throw new Exception("variable already exist:" + name);
        }

        public void Set(string name, int value)
        {
            if(IsExist(name))
            {
                INT.Remove(name);
                INT.Add(name, value);
                return;
            }
            throw new Exception("variable don't exist:" + name);
        }

        public int Get(string name)
        {
            if(IsExist(name))
            {
                return INT[name];
            }
            throw new Exception("variable don't exist:" + name);
        }

        public bool IsExist(String key)
        {
            return INT.ContainsKey(key) || REAL.ContainsKey(key) || BOOL.ContainsKey(key);
        }
    }
}