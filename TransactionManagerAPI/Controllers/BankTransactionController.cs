using Application.Cases.BankTransactionCases;
using AutoMapper;
using Core.Domains;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransactionManagerAPI.Dto;
using TransactionManagerAPI.Dtos;

namespace TransactionManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankTransactionController : ControllerBase
    {
        private readonly ICreateBankTransactionUseCase _createBankTransactionUseCase;

        private readonly IMapper _mapper;

        public BankTransactionController(ICreateBankTransactionUseCase createBankTransactionUseCase, IMapper mapper)
        {
            _createBankTransactionUseCase = createBankTransactionUseCase;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Create(BankTransactionRequest bankTransactionRequest)
        {
            try
            {
                var bankTransaction = _mapper.Map<BankTransaction>(bankTransactionRequest);
                await _createBankTransactionUseCase.Execute(bankTransaction);
                return Ok(new BankResponse
                {
                    Message = $"Bank transaction created with ID: {bankTransaction.Id}"
                });
            }
            catch(Exception e)
            {
                return StatusCode(((int)HttpStatusCode.InternalServerError),new BankResponse
                {
                    Message = $"An error occurred while creating the bank transaction due to: {e.Message}"
                });
            }
        }
    }
}
