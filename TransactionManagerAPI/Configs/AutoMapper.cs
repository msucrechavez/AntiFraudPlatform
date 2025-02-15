using AutoMapper;
using Core.Domains;
using TransactionManagerAPI.Dtos;

namespace Application.Configs
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<NewBankAccountRequest, BankAccount>();
            CreateMap<BankTransactionRequest, BankTransaction>();
        }
    }
}