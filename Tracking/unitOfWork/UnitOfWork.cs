using System;
using System.Threading.Tasks;
using Tracking.Data;
using Tracking.Repositories;

namespace Tracking.unitOfWork
{
    /// <summary>
    /// Implementation of the unit of work pattern for coordinating multiple repository operations.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IssueContext _context;
        private IIssueRepository _issueRepository;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The issue context.</param>
        public UnitOfWork(IssueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public IIssueRepository IssueRepository
        {
            get
            {
                if (_issueRepository == null)
                {
                    _issueRepository = new IssueRepository(_context);
                }
                return _issueRepository;
            }
        }

        /// <inheritdoc/>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
