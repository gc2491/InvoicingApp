namespace AspNET.Models.ResultModel
{
    public class QueryResModel<T>
    {
        public bool Succeeded { get; set; } = true;
        public string Error { get; set; } = null;
        public T Data { get; set; }
    }
}
