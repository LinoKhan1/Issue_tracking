using Tracking.Models;

namespace Tracking.Repositories
{
    /// <summary>
    /// Interface for managing issues.
    /// </summary>
    public interface IIssueRepository : IDisposable
    {
        /// <summary>
        /// Retrieves an issue by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the issue to retrieve.</param>
        /// <returns>The issue with the specified ID.</returns>
        Task<Issue> GetIssueByIdAsync(int id);
        /// <summary>
        /// Retrieves all issues asynchronously.
        /// </summary>
        /// <returns>A collection of all issues.</returns>
        Task<IEnumerable<Issue>> GetAllIssuesAsync();
        /// <summary>
        /// Adds a new issue asynchronously.
        /// </summary>
        /// <param name="issue">The issue to add.</param>

        Task AddIssueAsync(Issue issue);

        /// <summary>
        /// Updates an existing issue asynchronously.
        /// </summary>
        /// <param name="issue">The updated issue.</param>
        Task UpdateIssueAsync(Issue issue);
        /// <summary>
        /// Deletes an issue by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the issue to delete.</param>
        Task DeleteIssueAsync(int id);
        /// <summary>
        /// Checks if an issue with the specified ID exists asynchronously.
        /// </summary>
        /// <param name="id">The ID of the issue to check.</param>
        /// <returns>True if an issue with the specified ID exists; otherwise, false.</returns>
        Task<bool> IssueExists(int id);
    }

}
