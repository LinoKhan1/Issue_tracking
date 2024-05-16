using Microsoft.AspNetCore.Mvc;
using Moq;
using Tracking.Controllers;
using Tracking.Models;
using Tracking.unitOfWork;

namespace Tracking.tests.Controllers
{

    /// <summary>
    /// Unit tests for the <see cref="IssueController"/> class.
    /// </summary>
    public class IssueControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IssueController _issueController;

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueControllerTests"/> class.
        /// </summary>
        public IssueControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();  
            _issueController = new IssueController(_mockUnitOfWork.Object); 
        }


        [Fact]
        public async Task Index_ReturnsViewWithIssues_WhenNoSearchString()
        {
            // Arrange
            var issues = new List<Issue>
            {
                new Issue { Id = 1, Title = "Test Issue 1", Description = "Description 1", Status = "Open", Assignment = "John Doe", Priority = "High" },
                new Issue { Id = 2, Title = "Test Issue 2", Description = "Description 2", Status = "In Progress", Assignment = "Jane Smith", Priority = "Medium" }
            };
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetAllIssuesAsync()).ReturnsAsync(issues);

            // Act
            var result = await _issueController.Index(null) as ViewResult;

            // Assert
            var model = result.Model as IEnumerable<Issue>;
            Assert.NotNull(model);
            Assert.Equal(issues.Count, model.Count());
        }

        [Fact]
        public async Task Index_ReturnsViewWithFilteredIssues_WhenSearchStringProvided()
        {
            // Arrange
            var searchString = "Test";
            var issues = new List<Issue>
            {
                new Issue { Id = 1, Title = "Test Issue 1", Description = "Description 1", Status = "Open", Assignment = "John Doe", Priority = "High" },
                new Issue { Id = 2, Title = "Test Issue 2", Description = "Description 2", Status = "In Progress", Assignment = "Jane Smith", Priority = "Medium" }
            };
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetAllIssuesAsync()).ReturnsAsync(issues);

            // Act
            var result = await _issueController.Index(searchString) as ViewResult;

            // Assert
            var model = result.Model as IEnumerable<Issue>;
            Assert.NotNull(model);
            Assert.True(model.All(i => i.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       i.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       i.Status.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       i.Assignment.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       i.Priority.Contains(searchString, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Tests the Details action to verify it returns the correct view with the specified issue.
        /// </summary>
        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _issueController.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }


        /// <summary>
        /// Tests the Details action to verify it returns NotFound when an invalid ID is provided.
        /// </summary>

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIssueIsNull()
        {
            // Arrange
            int id = 1;
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetIssueByIdAsync(id)).ReturnsAsync((Issue)null);

            // Act
            var result = await _issueController.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the Create action (GET) to verify it returns the correct view.
        /// </summary>
        [Fact]
        public void Create_ReturnsView()
        {
            // Act
            var result = _issueController.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the Create action (POST) to verify it redirects to the Index action when the model is valid.
        /// </summary>
        [Fact]
        public async Task Create_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var validIssue = new Issue { Title = "Test Issue", Description = "Test Description", Status = "Open", Assignment = "John Doe", Priority = "High" };
            _mockUnitOfWork.Setup(u => u.IssueRepository.AddIssueAsync(It.IsAny<Issue>())).Verifiable();

            // Act
            var result = await _issueController.Create(validIssue) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


        /// <summary>
        /// Tests the Create action (POST) to verify it returns the view with the model when the model is invalid.
        /// </summary>
        [Fact]
        public async Task Create_WithInvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            var invalidIssue = new Issue { Title = null }; // Invalid model

            // AddModelError needs ModelState to be valid for the test to be meaningful
            _issueController.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await _issueController.Create(invalidIssue) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidIssue, result.Model); // Check if the model is passed back to the view
        }

        /// <summary>
        /// Tests the Edit action (GET) to verify it returns the correct view with the specified issue.
        /// </summary>
        [Fact]
        public async Task Edit_WithValidId_ReturnsViewWithIssue()
        {
            // Arrange
            var issueId = 1;
            var mockIssue = new Issue { Id = issueId, /* Valid properties */ };
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetIssueByIdAsync(issueId)).ReturnsAsync(mockIssue);

            // Act
            var result = await _issueController.Edit(issueId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Issue>(viewResult.ViewData.Model);
            Assert.Equal(mockIssue, model);
        }

        /// <summary>
        /// Tests the Edit action (GET) to verify it returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public async Task Edit_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var issueId = 1;
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetIssueByIdAsync(issueId)).ReturnsAsync((Issue)null);

            // Act
            var result = await _issueController.Edit(issueId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Tests the Edit action (POST) to verify it redirects to the Index action when the model is valid.
        /// </summary>
        [Fact]
        public async Task Edit_Post_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var issueId = 1;
            var mockIssue = new Issue { Id = issueId, /* Valid properties */ };
            _mockUnitOfWork.Setup(u => u.IssueRepository.UpdateIssueAsync(mockIssue)).Verifiable();

            // Act
            var result = await _issueController.Edit(issueId, mockIssue);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        /// <summary>
        /// Tests the Edit action (POST) to verify it returns the view with the model when the model is invalid.
        /// </summary>
        [Fact]
        public async Task Edit_Post_WithInvalidModel_ReturnsView()
        {
            // Arrange
            var issueId = 1;
            var mockIssue = new Issue { Id = issueId, /* Invalid properties */ };
            _issueController.ModelState.AddModelError("PropertyName", "Error Message");

            // Act
            var result = await _issueController.Edit(issueId, mockIssue);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(mockIssue, viewResult.ViewData.Model);
        }

        /// <summary>
        /// Tests the Delete action (GET) to verify it returns the correct view with the specified issue.
        /// </summary>
        [Fact]
        public async Task Delete_WithValidId_ReturnsViewWithIssue()
        {
            // Arrange
            var issueId = 1;
            var mockIssue = new Issue { Id = issueId, Title = "Test Issue" };
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetIssueByIdAsync(issueId)).ReturnsAsync(mockIssue);

            // Act
            var result = await _issueController.Delete(issueId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Issue>(viewResult.ViewData.Model);
            Assert.Equal(mockIssue, model);
        }

        /// <summary>
        /// Tests the Delete action (GET) to verify it returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var issueId = 1;
            _mockUnitOfWork.Setup(u => u.IssueRepository.GetIssueByIdAsync(issueId)).ReturnsAsync((Issue)null);

            // Act
            var result = await _issueController.Delete(issueId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Tests the DeleteConfirmed action to verify it redirects to the Index action after successful deletion.
        /// </summary>
        [Fact]
        public async Task DeleteConfirmed_ValidId_RedirectsToIndex()
        {
            // Arrange
            var issueId = 1;
            _mockUnitOfWork.Setup(u => u.IssueRepository.DeleteIssueAsync(issueId)).Verifiable();

            // Act
            var result = await _issueController.DeleteConfirmed(issueId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockUnitOfWork.Verify(u => u.IssueRepository.DeleteIssueAsync(issueId), Times.Once);
        }

        /// <summary>
        /// Tests the DeleteConfirmed action to verify it returns NotFound when an invalid ID is provided.
        /// </summary>
        [Fact]
        public async Task DeleteConfirmed_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var issueId = 1;
            _mockUnitOfWork.Setup(u => u.IssueRepository.DeleteIssueAsync(issueId)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _issueController.DeleteConfirmed(issueId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
