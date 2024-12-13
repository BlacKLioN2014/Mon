namespace Mon.Models
{
    public class Response<T>
    {
        public bool success { get; set; }

        public T message { get; set; }
    }

}
