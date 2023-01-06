using NearToEndpointDtos.BankAccounts;

namespace ApplicationService.BankAccounts
{
    public interface IBankAccountApplicationService
    {
        Task<BankAccountDto> AddBankAccountAsync(AddBankAccountDto addBankAccountDto);
        Task<BankAccountDto> Get(long key);
    }
}