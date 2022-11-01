namespace AccountsService.Core.Exceptions
{
    public enum DomainErrorCode
    {
        NotFound = 1000,
        InvalidArgument = 1001,
        InvalidOperation = 1002,
        InvalidCurrency = 1003
    }

    public class DomainException : Exception
    {
        public DomainErrorCode ErrorCode { get; }

        public DomainException(DomainErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
