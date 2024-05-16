using Microsoft.EntityFrameworkCore;
using Tracking.Data;
using Tracking.Models;

namespace Tracking.Repositories
{
    /// <summary>
    /// Repository for managing issues.
    /// </summary>
    public class IssueRepository: RepositoryBase, IIssueRepository
    {
        private readonly IssueContext _context;
        private bool disposedValue;


        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public IssueRepository(IssueContext context): base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        /// <inheritdoc/>

        public async Task<Issue> GetIssueByIdAsync(int id)
        {
            return await _context.Issue.FindAsync(id);

        }

        /// <inheritdoc/>

        public async Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            return await _context.Issue.ToListAsync();
        }
        /// <inheritdoc/>

        public async Task AddIssueAsync(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException(nameof(issue));
            }

            _context.Issue.Add(issue);
            await _context.SaveChangesAsync();
        }
        /// <inheritdoc/>

        public async Task UpdateIssueAsync(Issue issue)
        {
            if (issue == null)
            {
                throw new ArgumentNullException(nameof(issue));
            }

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <inheritdoc/>

        public async Task DeleteIssueAsync(int id)
        {
            var issueToDelete = await _context.Issue.FindAsync(id);
            if (issueToDelete != null)
            {
                _context.Issue.Remove(issueToDelete);
                await _context.SaveChangesAsync();
            }
        }
        /// <inheritdoc/>

        public async Task<bool> IssueExists(int id)
        {
            return await _context.Issue.AnyAsync(e => e.Id == id);
        }


        /// <inheritdoc/>

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~IssueRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
