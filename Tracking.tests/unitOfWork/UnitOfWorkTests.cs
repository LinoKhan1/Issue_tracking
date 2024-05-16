using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data;
using Tracking.unitOfWork;

namespace Tracking.tests.unitOfWork
{
    public class UnitOfWorkTests
    {
        [Fact]
        public void Constructr_InitializesContext()
        {
            // Arrange
            var mockContext = new Mock<IssueContext>();

            // Act
            var unitOfWork  = new UnitOfWork(mockContext.Object);   

            // Assert
            Assert.NotNull(unitOfWork);
            Assert.Equal(mockContext.Object, unitOfWork.GetType().GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(unitOfWork));
        }

        [Fact]
        public void IssueRepository_Returns_Expected_Instance()
        {
            // Arrange
            var mockContext = new Mock<IssueContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var issueRepository1 = unitOfWork.IssueRepository;
            var issueRepository2 = unitOfWork.IssueRepository;

            // Assert
            Assert.NotNull(issueRepository1);
            Assert.NotNull(issueRepository2);
            Assert.Same(issueRepository1, issueRepository2);
        }

        [Fact]
        public async Task CompleteAsync_SavesChanges()
        {
            // Arrange
            var mockContext = new Mock<IssueContext>();
            mockContext.Setup(c => c.SaveChangesAsync(default(CancellationToken))).ReturnsAsync(1); // Specify the default value for the optional CancellationToken parameter
            var unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            var result = await unitOfWork.CompleteAync();

            // Assert
            Assert.Equal(1, result);
        }

        /*[Fact]
        public void Dispose_Calls_Dispose_On_Context()
        {
            // Arrange
            var mockContext = new Mock<IssueContext>();
            var unitOfWork = new UnitOfWork(mockContext.Object);

            // Act
            unitOfWork.Dispose();

            // Assert
            mockContext.Verify(c => c.Dispose(), Times.Once);
        }
        */

    }
}
