using AracKiralamaPortal.Data.Interfaces;
using AracKiralamaPortal.Data.Repositories;

namespace AracKiralamaPortal.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Cars = new GenericRepository<Models.Car>(_context);
            Reservations = new GenericRepository<Models.Reservation>(_context);
            Payments = new GenericRepository<Models.Payment>(_context);
        }

        public IRepository<Models.Car> Cars { get; private set; }
        public IRepository<Models.Reservation> Reservations { get; private set; }
        public IRepository<Models.Payment> Payments { get; private set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Models.Car> Cars { get; }
        IRepository<Models.Reservation> Reservations { get; }
        IRepository<Models.Payment> Payments { get; }
        Task SaveAsync();
    }
}
