using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data;
using Tracking.Models;
using Tracking.Repositories;
using Tracking.tests.Fixture;

namespace Tracking.tests.Repositories
{
    public  class IssueRepositoryIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly IssueContext _context;
        private readonly IssueRepository _repository;

        public IssueRepositoryIntegrationTests(TestFixture fixture)
        {
            _context = fixture.Context;
            _repository = new IssueRepository(_context);
        }

        [Fact]
        public async Task GetAllIssuesAsync_ReturnsAllIssues()
        {
            // Act
            var issues = await _repository.GetAllIssuesAsync();

            // Assert
            Assert.Equal(2, issues.Count());
        }

        [Fact]
        public async Task GetIssueByIdAsync_ValidId_ReturnsIssue()
        {
            // Arrange
            var existingIssueId = _context.Issue.First().Id;

            // Act
            var issue = await _repository.GetIssueByIdAsync(existingIssueId);

            // Assert
            Assert.NotNull(issue);
            Assert.Equal(existingIssueId, issue.Id);
        }

        [Fact]
        public async Task GetIssueByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidIssueId = -1;

            // Act
            var issue = await _repository.GetIssueByIdAsync(invalidIssueId);

            // Assert
            Assert.Null(issue);
        }

        [Fact]
        public async Task AddIssueAsync_AddsIssueToDatabase()
        {
            // Arrange
            var issue = new Issue { Title = "Issue 3", Description = "Description 3", Status = "Pending", Assignment = "User3", Priority = "Low" };

            // Act
            await _repository.AddIssueAsync(issue);
            var issues = await _repository.GetAllIssuesAsync();

            // Assert
            Assert.Equal(3, issues.Count());
            Assert.Contains(issues, i => i.Title == "Issue 3");
        }

        [Fact]
        public async Task UpdateIssueAsync_UpdatesIssueInDatabase()
        {
            // Arrange
            var issue = _context.Issue.First();
            issue.Title = "Updated Title";

            // Act
            await _repository.UpdateIssueAsync(issue);
            var updatedIssue = await _repository.GetIssueByIdAsync(issue.Id);

            // Assert
            Assert.Equal("Updated Title", updatedIssue.Title);
        }

        [Fact]
        public async Task DeleteIssueAsync_DeletesIssueFromDatabase()
        {
            // Arrange
            var issue = _context.Issue.First();

            // Act
            await _repository.DeleteIssueAsync(issue.Id);
            var issues = await _repository.GetAllIssuesAsync();

            // Assert
            Assert.Equal(1, issues.Count());
            Assert.DoesNotContain(issues, i => i.Id == issue.Id);
        }

        [Fact]
        public async Task IssueExists_ValidId_ReturnsTrue()
        {
            // Arrange
            var existingIssueId = _context.Issue.First().Id;

            // Act
            var exists = await _repository.IssueExists(existingIssueId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task IssueExists_InvalidId_ReturnsFalse()
        {
            // Arrange
            var invalidIssueId = -1;

            // Act
            var exists = await _repository.IssueExists(invalidIssueId);

            // Assert
            Assert.False(exists);
        }

    }
}
