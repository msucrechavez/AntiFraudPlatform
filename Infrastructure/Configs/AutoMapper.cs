using AutoMapper;
using Core.Domains;
using Infrastructure.Entities;

namespace Application.Configs
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<BankAccount, BankAccountEntity>();
            CreateMap<BankAccountEntity, BankAccount>();
            CreateMap<BankTransaction, BankTransactionEntity>();
            CreateMap<BankTransactionEntity, BankTransaction>();
        }
    }
}