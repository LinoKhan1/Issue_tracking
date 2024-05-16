namespace Tracking.Models
{
    /// <summary>
    /// Represents an issue in the tracking system.
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the unique identifier for the issue.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the title of the issue.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description of the issue.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the status of the issue (e.g., Open, In Progress, Closed).
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Gets or sets the assignment of the issue (e.g., Assigned User or Team).
        /// </summary>
        public string Assignment { get; set; }

        /// <summary>
        /// Gets or sets the priority of the issue (e.g., High, Medium, Low).
        /// </summary>
        public string Priority { get; set; }    
    }
}
