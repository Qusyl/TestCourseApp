using Application;
using Application.Interface;
using Domain;
using Infrastructure.Persistance;


namespace Infrastructure.BackgroundService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context) => _context = context;
        public Task<Result<ApplicationError>> SaveChangesAsync()
        {
            
        }
    }
}
