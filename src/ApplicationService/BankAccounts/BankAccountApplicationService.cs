using Domain.BankAccounts;
using Infrastructure.Persistence.Mongo.RepositoryAbstractions;
using WritableBankAccount = Infrastructure.Persistence.MSSQL.Models.BankAccount;
using ReadableBankAccount = Infrastructure.Persistence.Mongo.Models.BankAccount;
using Infrastructure.Persistence.MSSQL.RepositoryAbstractions;
using Mapster;
using NearToEndpointDtos.BankAccounts;

namespace ApplicationService.BankAccounts
{
    public class BankAccountApplicationService: IBankAccountApplicationService
    {
        private readonly IBankAccountReadableRepository _bankAccountReadableRepository;
        private readonly IBankAccountWritableRepository _bankAccountWritableRepository;

        public BankAccountApplicationService(IBankAccountReadableRepository bankAccountReadableRepository, IBankAccountWritableRepository bankAccountWritableRepository)
        {
            _bankAccountReadableRepository = bankAccountReadableRepository;
            _bankAccountWritableRepository = bankAccountWritableRepository;
        }

        public async Task<BankAccountDto> AddBankAccountAsync(AddBankAccountDto addBankAccountDto)
        {
            try
            {
                var bankAccounts = await _bankAccountReadableRepository.SearchForBankAccounts(x => x.FullName == addBankAccountDto.CustomerFullName.Trim().ToLower()
                       && x.DateOfBirth == addBankAccountDto.DateOdBirth, x => x.Id);
                if (bankAccounts.Any())
                {
                    throw new ArgumentException("there is a bank account with same owner full name, and date of birth");
                }

                var bankAccountDomain = new BankAccountDomainBuilder()
                    .With(x => x.AccountNumber, addBankAccountDto.BankAccountNumber)
                    .With(x => x.CreateDateTime, addBankAccountDto.CreateDateTime)
                    .With(x => x.UpdateDateTime, addBankAccountDto.UpdateDateTime)
                    .With(x => x.CustomerVo,
                        new CustomerVo(addBankAccountDto.CustomerId, addBankAccountDto.CustomerFullName,
                            addBankAccountDto.DateOdBirth))
                    .Build();

                var persistingWritableBankAccount = bankAccountDomain.Adapt<WritableBankAccount>();
                var addedBankAccountToWritableRepo =
                    await _bankAccountWritableRepository.Add(persistingWritableBankAccount);

                var persistingReadableBankAccount = bankAccountDomain.Adapt<ReadableBankAccount>();
                await _bankAccountReadableRepository.Add(persistingReadableBankAccount);

                var outputBankAccountDto = persistingReadableBankAccount.Adapt<BankAccountDto>();
                return outputBankAccountDto;
            }
            catch (Exception e)
            {
                return new BankAccountDto()
                {
                    ErrorMessage = e.Message
                };
            }
        }

        public Task<BankAccountDto> Get(long key)
        {
            throw new NotImplementedException();
        }
    }
}
