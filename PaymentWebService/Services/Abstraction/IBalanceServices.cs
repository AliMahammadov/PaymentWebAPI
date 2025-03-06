namespace PaymentWebService.Services.Abstraction
{
    public interface IBalanceServices
    {
        Task AddBalanceAsync(int userId, decimal amount);
    }
}
