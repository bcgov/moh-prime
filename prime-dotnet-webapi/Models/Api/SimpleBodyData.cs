namespace Prime.Models.Api
{
    public struct FromBodyData<T> where T : struct
    {
        private readonly T _data;
        public T Data { get { return _data; } }

        public FromBodyData(T data)
        {
            _data = data;
        }

        // public static implicit operator FromBodyData<T>(T t)
        // {
        //     return new FromBodyData<T>(t);
        // }

        public static implicit operator T(FromBodyData<T> t)
        {
            return t.Data;
        }
    }

    public struct FromBodyText
    {
        private readonly string _data;
        public string Data { get { return _data; } }

        public FromBodyText(string data)
        {
            _data = data;
        }

        // public static implicit operator FromBodyText(string s)
        // {
        //     return new FromBodyText(s);
        // }

        public static implicit operator string(FromBodyText t)
        {
            return t.Data;
        }
    }
}
