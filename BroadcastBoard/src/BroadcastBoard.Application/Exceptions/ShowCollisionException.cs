namespace BroadcastBoard.Application.Shows.Exceptions
{
    public class ShowCollisionException : Exception
    {
        public ShowCollisionException(string message) : base(message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }
    }
}
