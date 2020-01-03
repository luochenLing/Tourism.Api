namespace Tourism.Api.Model
{
    public class ResultObject<T> where T : class
    {
        public int code { get; set; }

        public string msg { get; set; }

        public dynamic resultData { get; set; }
    }
}
