namespace VoxTenouApp.Models
{
    public class ApiException : Exception
    {
        private readonly string message;
        public ApiException(string message) { this.message = message; }
        public override string Message => this.message;
    }
}
