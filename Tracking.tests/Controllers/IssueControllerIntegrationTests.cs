using Microsoft.AspNetCore.Mvc;
using Tracking.Controllers;
using Tracking.Models;
using Tracking.tests.Fixture;
using Xunit;

namespace Tracking.Tests
{
    /// <summary>
    /// Integration tests for the IssueController.
    /// </summary>
    public class IssueControllerIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;
        private readonly IssueController _controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueControllerIntegrationTests"/> class.
        /// </summary>
        /// <param name="fixture">The test fixture.</param>
        public IssueControllerIntegrationTests(TestFixture fixture)
        {
            _fixture = fixture;
            _controller = _fixture.IssueController;
        }

        /// <summary>
        /// Tests the Index action returns a view with a list of issues.
        /// </summary>
        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfIssues()
        {
            // Act
            var result = await _controller.Index(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Issue>>(result.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        /// <summary>
        /// Tests the Details action returns a view with the correct issue.
        /// </summary>
        [Fact]
        public async Task Details_ReturnsViewResult_WithIssue()
        {
            // Act
            var result = await _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<Issue>(result.ViewData.Model);
            Assert.Equal(1, model.Id);
        }

        /// <summary>
        /// Tests the Create action redirects to the Index on successful creation.
        /// </summary>
        [Fact]
        public async Task Create_Post_RedirectsToIndex_OnSuccess()
        {
            // Arrange
            var issue = new Issue { Title = "New Issue", Description = "New Description", Status = "Open", Assignment = "User3", Priority = "Low" };

            // Act
            var result = await _controller.Create(issue) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(3, _fixture.Context.Issue.Count());
        }

        /// <summary>
        /// Tests the Edit action updates the issue and redirects to Index.
        /// </summary>
        [Fact]
        public async Task Edit_Post_UpdatesIssue_AndRedirectsToIndex()
        {
            // Arrange
            var issue = _fixture.Context.Issue.First();
            issue.Title = "Updated Title";

            // Act
            var result = await _controller.Edit(issue.Id, issue) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            var updatedIssue = await _fixture.Context.Issue.FindAsync(issue.Id);
            Assert.Equal("Updated Title", updatedIssue.Title);
        }

        /// <summary>
        /// Tests the Delete action removes the issue and redirects to Index.
        /// </summary>
        [Fact]
        public async Task DeleteConfirmed_DeletesIssue_AndRedirectsToIndex()
        {
            // Act
            var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal(1, _fixture.Context.Issue.Count());
        }

        /// <summary>
        /// Tests the Delete action returns NotFound for invalid ID.
        /// </summary>
        /*[Fact]
        public async Task DeleteConfirmed_InvalidId_ReturnsNotFound()
        {
            // Act
            var result = await _controller.DeleteConfirmed(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);*/
        }
    }
}
