using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookNookWebApp.Data;
using BookNookWebApp.Models;

namespace BookNookWebApp.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments
                .Include(c => c.Topic)
                .Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Topic)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create(int? topicId, int? parentCommentId)
        {
            ViewData["TopicId"] = topicId ?? 0;
            ViewData["ParentCommentId"] = parentCommentId ?? 0;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,TopicId,UserId,ParentCommentId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.PostedAt = DateTime.Now;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Topics", new { id = comment.TopicId });
            }

            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", comment.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            ViewData["ParentCommentId"] = comment.ParentCommentId;
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", comment.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            ViewData["ParentCommentId"] = comment.ParentCommentId;
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Content,PostedAt,TopicId,UserId,ParentCommentId")] Comment comment)
        {
            if (id != comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentId))
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

            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", comment.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", comment.UserId);
            ViewData["ParentCommentId"] = comment.ParentCommentId;
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Topic)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.CommentId == id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
