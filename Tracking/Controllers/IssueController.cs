using Microsoft.AspNetCore.Mvc;
using Tracking.Models;
using Tracking.unitOfWork;

namespace Tracking.Controllers
{
    /// <summary>
    /// Controller for managing issues.
    /// </summary>
    public class IssueController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Initializes a new instance of the <see cref="IssueController"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public IssueController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Displays a list of all issues.
        /// GET: Issue
        /// </summary>
        

        public async Task<IActionResult> Index(string searchString)
        {
            var issues = await _unitOfWork.IssueRepository.GetAllIssuesAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(s => s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Status.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Assignment.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                s.Priority.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            return View(issues);

        }
        /// <summary>
        /// Displays details of a specific issue.
        /// GET: Issue/Details/5
        /// </summary>
        /// <param name="id">The ID of the issue.</param>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _unitOfWork.IssueRepository.GetIssueByIdAsync(id.Value); 
            if(issue == null)
            {
                return NotFound();
            }
            return View(issue); 
        }
        /// <summary>
        /// Displays the form for creating a new issue.
        /// GET: Issue/Create
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Handles the POST request for creating a new issue.
        /// POST: Issue/Create
        /// </summary>
        /// <param name="issue">The issue to create.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Status,Assignment,Priority")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.IssueRepository.AddIssueAsync(issue);
                await _unitOfWork.CompleteAsync(); // Ensure to save changes to the database
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }
        /// <summary>
        /// Displays the form for editing an issue.
        /// GET: Issue/Edit/5
        /// </summary>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _unitOfWork.IssueRepository.GetIssueByIdAsync(id.Value);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }
        /// <summary>
        /// Handles the POST request for editing an issue.
        /// POST: Issue/Edit/5
        /// </summary>
        /// <param name="issue">The issue to create.</param>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Assignment,Priority")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.IssueRepository.UpdateIssueAsync(issue);
                }
                catch
                {
                    if (!await _unitOfWork.IssueRepository.IssueExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }
        /// <summary>
        /// Displays the form for deleting an issue.
        /// GET: Issue/Delete/5
        /// </summary>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _unitOfWork.IssueRepository.GetIssueByIdAsync(id.Value);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }


        /// <summary>
        /// Handles the POST request for deleting an issue.
        /// POST: Issue/Delete/5
        /// </summary>
        /// <param name="issue">The issue to create.</param> 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.IssueRepository.DeleteIssueAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
