namespace PaymentWebService.Services.Abstraction;

public interface IPaymetServices
{
        Task<bool> TransferBalanceAsync(string senderPhoneNumber, string receiverPhoneNumber, decimal amount);
    
}