using Tracking.Data;

namespace Tracking.Repositories
{
    /// <summary>
    /// Base class for repositories providing access to the database context.
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly IssueContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="context"/> is null.</exception>
        protected RepositoryBase(IssueContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
