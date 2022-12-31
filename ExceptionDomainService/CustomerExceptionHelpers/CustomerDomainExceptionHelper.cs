using DomainContract.Exceptions;

namespace ExceptionDomainService.CustomerExceptionHelpers
{
    public class CustomerDomainExceptionHelper : IDomainExceptionServiceProvider
    {
        public void ThrowExceptionMessage(int exceptionCode, string exceptionMessage)
        {
            var exception = new Exception(exceptionMessage)
            {
                HResult = exceptionCode
            };

            throw exception;
        }
    }
}
