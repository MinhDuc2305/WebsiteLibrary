using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Data;

namespace WebsiteLibrary.Controllers
{
    public class ReaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReaderController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        [Authorize(Roles = "Reader")] // Yêu cầu đăng nhập với vai trò Reader
        public IActionResult Info()
        {
            var email = User.Identity.Name; // Lấy email từ thông tin đăng nhập
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            var account = _context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account == null)
            {
                return NotFound("Không tìm thấy thông tin tài khoản.");
            }

            return View(account);
        }

        // GET: /Reader/Index
        [Authorize(Roles = "Reader")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
    }
}
