using Tracking.Repositories;

namespace Tracking.unitOfWork
{
    /// <summary>
    /// Interface representing a unit of work pattern for coordinating multiple repository operations.
    /// </summary>
    public interface IUnitOfWork: IDisposable
    {
        // <summary>
        /// Gets the issue repository.
        /// </summary>
        IIssueRepository IssueRepository { get; }

        /// <summary>
        /// Saves changes asynchronously and returns the number of affected rows.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
        Task<int> CompleteAsync();
    }
}
