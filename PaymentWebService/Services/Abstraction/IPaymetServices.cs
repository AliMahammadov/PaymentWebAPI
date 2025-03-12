namespace PaymentWebService.Services.Abstraction;

public interface IPaymetServices
{
        Task<bool> TransferBalanceAsync(int senderId, string receiverPhoneNumber, decimal amount);
    
}