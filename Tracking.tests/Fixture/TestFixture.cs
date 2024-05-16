using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Controllers;
using Tracking.Data;
using Tracking.Models;
using Tracking.unitOfWork;

namespace Tracking.tests.Fixture
{
    public class TestFixture : IDisposable
    {
        public IssueContext Context { get; private set; }
        public IssueController IssueController { get; private set; }

        public TestFixture()
        {
            var options = new DbContextOptionsBuilder<IssueContext>()
                .UseInMemoryDatabase(databaseName: "IssueDatabase")
                .Options;

            Context = new IssueContext(options);
            SeedDatabase();

            var unitOfWork = new UnitOfWork(Context);
            IssueController = new IssueController(unitOfWork);
        }

        private void SeedDatabase()
        {
            Context.Issue.AddRange(
                new Issue { Title = "Issue 1", Description = "Description 1", Status = "Open", Assignment = "User1", Priority = "High" },
                new Issue { Title = "Issue 2", Description = "Description 2", Status = "Closed", Assignment = "User2", Priority = "Medium" }
            );
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

}
