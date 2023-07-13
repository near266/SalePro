namespace OrderSvc.Domain.Abstractions
{
    public interface IOrderSvcDbContext
    {
       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
