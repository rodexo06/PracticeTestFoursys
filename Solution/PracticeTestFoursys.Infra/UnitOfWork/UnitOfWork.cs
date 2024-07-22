using PracticeTestFoursys.Application.Repositories;
using PracticeTestFoursys.Infra.Context;

namespace PracticeTestFoursys.Infra.UOW {
    public class UnitOfWork : IUnitOfWork {
        private bool _disposed;
        private readonly PositionContext _context;

        public UnitOfWork(PositionContext context) =>
            _context = context;

        public async Task<int> CommitAsync() =>
            await _context.SaveChangesAsync();

        protected virtual void DisposeAsync(bool disposing)
        {
            if (!_disposed && disposing)
                _context.DisposeAsync();

            _disposed = true;
        }

        public void Dispose()
        {
            DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
