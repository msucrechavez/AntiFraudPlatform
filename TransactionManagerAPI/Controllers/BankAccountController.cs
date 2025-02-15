using Application.Cases.BankAccountCases;
using AutoMapper;
using Core.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransactionManager.Application.Cases.BankAccountCases;
using TransactionManager.Application.Cases.BankAccountCases.Implementations;
using TransactionManagerAPI.Dto;
using TransactionManagerAPI.Dtos;

namespace TransactionManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly ICreateBankAccountUseCase _createBankAccountUseCase;

        private readonly IFindSingleBankAccountUseCase _findSingleBankAccountUseCase;

        private readonly IListAllBankAccountsUseCase _listAllBankAccountsUseCase;

        private readonly IFindBankAccountTransactionsUseCase _findBankAccountTransactionsUseCase;

        private readonly IMapper _mapper;

        public BankAccountController(ICreateBankAccountUseCase createBankAccountUseCase,
            IFindSingleBankAccountUseCase findSingleBankAccountUseCase,
            IListAllBankAccountsUseCase listAllBankAccountsUseCase,
            IFindBankAccountTransactionsUseCase findBankAccountTransactionsUseCase,
            IMapper mapper)
        {
            _createBankAccountUseCase = createBankAccountUseCase;
            _findSingleBankAccountUseCase = findSingleBankAccountUseCase;
            _listAllBankAccountsUseCase = listAllBankAccountsUseCase;
            _findBankAccountTransactionsUseCase = findBankAccountTransactionsUseCase;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Create(NewBankAccountRequest bankAccountRequest)
        {
            try
            {
                var bankAccount = _mapper.Map<BankAccount>(bankAccountRequest);
                await _createBankAccountUseCase.Execute(bankAccount);
                return Ok(new BankResponse
                {
                    Message = $"Bank account created successfully with ID: {bankAccount.Id}"
                });
            }
            catch (Exception e)
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError), new BankResponse
                {
                    Message = $"An error occurred while creating the bank account due to: {e.Message}"
                });
            }
        }


        [HttpGet]
        [Route("/{bankAccountId}")]
        public async Task<IActionResult> Get(Guid bankAccountId)
        {
            var bankAccount = await _findSingleBankAccountUseCase.Execute(bankAccountId);
            return Ok(bankAccount);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var bankAccounts = await _listAllBankAccountsUseCase.Execute();
            return Ok(bankAccounts);
        }

        [HttpGet]
        [Route("/{bankAccountId}/transactions")]
        public async Task<IActionResult> GetTransactions(Guid bankAccountId)
        {
            var bankAccount = await _findBankAccountTransactionsUseCase.Execute(bankAccountId);
            return Ok(bankAccount);
        }

    }
}
