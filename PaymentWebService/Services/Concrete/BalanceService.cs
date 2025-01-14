using PaymentWebData.DAL;
using PaymentWebEntity.DTOs.BalancrDTOs;
using PaymentWebEntity.Entities;
using PaymentWebService.Services.Abstraction;

namespace PaymentWebService.Services.Concrete
{
    public class BalanceService : IBalanceServices
    {
        private readonly AppDbContext _dbContext;

        public BalanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateBalanceAsync(BalanceDTO balanceDto)
        {
            var balance = new Balance
            {
            };
            throw new NotImplementedException();
        }

        public Task DeleteBalanceAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<BalanceDTO> GetBalanceAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBalanceAsync(BalanceDTO balanceDto)
        {
            throw new NotImplementedException();
        }
    }
}
