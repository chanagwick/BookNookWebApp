using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookNookWebApp.Data;
using BookNookWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BookNookWebApp.Controllers
{
    public class ForumPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ForumPostsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ForumPosts
        public async Task<IActionResult> Index()
        {
            var forumPosts = await _context.ForumPosts
                .Include(f => f.Topic)  // Include the related Topic
                .Include(f => f.User)   // Include the related User
                .ToListAsync();         // Get the list of ForumPosts

            return View(forumPosts);  // Return the list to the view
        }

        // GET: ForumPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts
                .Include(f => f.Topic)
                .Include(f => f.User)
                .Include(f => f.Comments)  // Include the comments for this post
                .ThenInclude(c => c.User)  // Include the user who wrote the comment
                .FirstOrDefaultAsync(m => m.ForumPostId == id);

            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);  // Pass the ForumPost to the view
        }

        // POST: ForumPosts/AddComment/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return RedirectToAction(nameof(Details), new { id = postId });
            }

            var forumPost = await _context.ForumPosts.FindAsync(postId);
            if (forumPost == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var comment = new Comment
            {
                Content = content,
                UserId = userId,
                ForumPostId = postId,
                PostedAt = DateTime.UtcNow
            };

            forumPost.Comments.Add(comment); // Add the comment directly to the Comments collection
            _context.Comments.Add(comment);  // Add the comment to the DbContext
            await _context.SaveChangesAsync(); // Save the changes

            return RedirectToAction(nameof(Details), new { id = postId }); // Redirect to Details
        }

        // GET: ForumPosts/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ForumPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumPostId,Title,Content,CreatedAt,UserId,TopicId")] ForumPost forumPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", forumPost.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", forumPost.UserId);
            return View(forumPost);
        }

        // GET: ForumPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost == null)
            {
                return NotFound();
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", forumPost.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", forumPost.UserId);
            return View(forumPost);
        }

        // POST: ForumPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumPostId,Title,Content,CreatedAt,UserId,TopicId")] ForumPost forumPost)
        {
            if (id != forumPost.ForumPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumPostExists(forumPost.ForumPostId))
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Title", forumPost.TopicId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", forumPost.UserId);
            return View(forumPost);
        }

        // GET: ForumPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumPost = await _context.ForumPosts
                .Include(f => f.Topic)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.ForumPostId == id);
            if (forumPost == null)
            {
                return NotFound();
            }

            return View(forumPost);
        }

        // POST: ForumPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost != null)
            {
                _context.ForumPosts.Remove(forumPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumPostExists(int id)
        {
            return _context.ForumPosts.Any(e => e.ForumPostId == id);
        }
    }
}
