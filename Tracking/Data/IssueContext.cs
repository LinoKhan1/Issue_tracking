using Microsoft.EntityFrameworkCore;
using Tracking.Models;

namespace Tracking.Data
{
    /// <summary>
    /// Represents the database context for managing issues.
    /// </summary>
    public class IssueContext : DbContext
    {

        public IssueContext()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public IssueContext(DbContextOptions<IssueContext> options) : base(options) { }
        /// <summary>
        /// Gets or sets the DbSet of issues.
        /// </summary>
        public DbSet<Tracking.Models.Issue> Issue { get; set; } = default!;

        // Other DbSets and configuration methods can be added here if needed


    }
}
