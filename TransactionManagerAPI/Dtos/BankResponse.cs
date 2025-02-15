namespace TransactionManagerAPI.Dto
{
    public class BankResponse
    {
        public required string Message { get; set; }

        public Dictionary<string, Object> Data { get; set; }
    }
}
