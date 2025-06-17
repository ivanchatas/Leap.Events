namespace Application.Wrapper
{
    public class Result<T>
    {
        public bool Succeeded { get; set; } = true;

        public List<string> Messages { get; set; }

        public T Data { get; set; }
    }
}
