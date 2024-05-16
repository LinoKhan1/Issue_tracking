using Microsoft.EntityFrameworkCore;
using Tracking.Data;
using Tracking.Models;
using Tracking.Repositories;

namespace Tracking.tests.Repositories
{
    public class IssueRepositoryTests
    {

        private readonly DbContextOptions<IssueContext> _options;
        private readonly IssueContext _context;
        private readonly IssueRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRepositoryTests"/> class.
        /// </summary>
        public IssueRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<IssueContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new IssueContext(_options);
            _repository = new IssueRepository(_context);
        }

        /// <summary>
        /// Creates and adds a test issue to the context.
        /// </summary>
        /// <param name="title">The title of the issue.</param>
        /// <param name="description">The description of the issue.</param>
        /// <returns>The created <see cref="Issue"/>.</returns>

        private async Task<Issue> CreateAndAddTestIssue(string title = "Test Issue", string description = "Test Description", string status = "Test Status", string Assignment = "Test Assignment", string Priority = "Test Priority")
        {
            var issue = new Issue { Title = title, Description = description, Status = status, Assignment = Assignment, Priority = Priority };
            _context.Issue.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }
        /// <summary>
        /// Tests if initializing with a null context throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        [Fact]
        public void Initialize_WithNullContext_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new IssueRepository(null));

        }
        /// <summary>
        /// Tests adding a valid issue.
        /// </summary>
        [Fact]
        public async Task AddIssueAsync_WithValidIssue_AddsIssue()
        {
            var issue = new Issue { Title = "Test Issue", Description = "Test Description", Status = "Test Status", Assignment = "Test Assignment", Priority = "Test Priority" };
            await _repository.AddIssueAsync(issue);
            var result = await _context.Issue.FirstOrDefaultAsync(i => i.Title == issue.Title); 
            Assert.NotNull(result); 
        }
        /// <summary>
        /// Tests adding a null issue throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        [Fact]
        public async Task AddIssueAsync_WithNullIssue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddIssueAsync(null));
        }
        /// <summary>
        /// Tests getting an issue by a valid ID.
        /// </summary>
        [Fact]
        public async Task GetIssueByIdAsync_WithValidId_ReturnsIssue()
        {
            var issue = await CreateAndAddTestIssue();

            var result = await _repository.GetIssueByIdAsync(issue.Id); 
            Assert.NotNull(result);
            Assert.Equal(issue.Title, result.Title);
        }

        /// <summary>
        /// Tests getting an issue by an invalid ID returns null.
        /// </summary>
        [Fact]
        public async Task GetIssueByIdAsync_WithInvalidId_ReturnsNull()
        {
            var result = await _repository.GetIssueByIdAsync(-1);
            Assert.Null(result);
        }

        /// <summary>
        /// Tests getting all issues.
        /// </summary>
        [Fact]
        public async Task GetAllIssuesAsync_ReturnsAllIssues()
        {
            var issues = new List<Issue>
        {
            await CreateAndAddTestIssue("Test Issue 1", "Test Description 1", "Test Status 1", "Test Assignment 1", "Test Priority 1"),
            await CreateAndAddTestIssue("Test Issue 2", "Test Description 2", "Test Status 2", "Test Assignment 2", "Test Priority 2")
        };

            var result = await _repository.GetAllIssuesAsync(); 
            Assert.Equal(2, result.Count() );


        }
        /// <summary>
        /// Tests updating a valid issue.
        /// </summary>
        [Fact]
        public async Task UpdateIssueAsync_WithValidIssue_UpdatesIssue()
        {
            var issue = await CreateAndAddTestIssue();
            issue.Title = "Updated Title";
            await _repository.UpdateIssueAsync(issue);

            var result = await _context.Issue.FirstOrDefaultAsync(i => i.Id == issue.Id);
            Assert.Equal("Updated Title", result.Title);
        }

        /// <summary>
        /// Tests updating a null issue throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        [Fact]
        public async Task UpdateIssueAsync_WithNullIssue_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.UpdateIssueAsync(null));
        }

        /// <summary>
        /// Tests deleting an issue by a valid ID.
        /// </summary>
        [Fact]
        public async Task DeleteIssueAsync_WithValidId_DeleteIssue()
        {
            var issue = await CreateAndAddTestIssue();
            await _repository.DeleteIssueAsync(issue.Id);

            var result = await _context.Issue.FirstOrDefaultAsync(i => i.Id == issue.Id);
            Assert.Null(result);

        }
        /// <summary>
        /// Tests deleting an issue by an invalid ID does nothing.
        /// </summary>
        [Fact]
        public async Task DeleteIssueAsync_WithInvalidId_DoesNothing()
        {
            var initialCount = await _context.Issue.CountAsync();
            await _repository.DeleteIssueAsync(-1);
            var finalCount = await _context.Issue.CountAsync();
            Assert.Equal(initialCount, finalCount);
        }

        /// <summary>
        /// Tests if an issue exists with a valid ID.
        /// </summary>
        [Fact]
        public async Task IssueExists_WithValidId_ReturnsTrue()
        {
            var issue = await CreateAndAddTestIssue();
            var result = await _repository.IssueExists(issue.Id);
            Assert.True(result);
        }

        /// <summary>
        /// Tests if an issue exists with an invalid ID returns false.
        /// </summary>
        [Fact]
        public async Task IssueExists_WithInvalidId_ReturnsFalse()
        {
            var result = await _repository.IssueExists(-1);
            Assert.False(result);
        }

        /// <summary>
        /// Tests disposing the repository.
        /// </summary>
        [Fact]
        public void Dispose_DisposesWithoutException()
        {
            var repository = new IssueRepository(_context);
            var ex = Record.Exception(() => repository.Dispose());
            Assert.Null(ex);
        }

    }
}