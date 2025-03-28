using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Data;
using WebsiteLibrary.Models.Entities;

namespace WebsiteLibrary.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Readers()
        {
            return View();
        }

        // Lấy danh sách độc giả
        [HttpGet]
        public async Task<IActionResult> GetReaders()
        {
            var readers = await _context.Readers
                .Select(r => new
                {
                    id = r.ReaderCode,
                    name = r.Name,
                    dob = r.DateOfBirth.ToString("dd/MM/yyyy"),
                    gender = r.Gender,
                    phone = r.PhoneNumber,
                    email = r.Email,
                    address = r.Address,
                    education = r.EducationLevel
                })
                .ToListAsync();

            return Json(readers);
        }

        // Thêm độc giả
        [HttpPost]
        public async Task<IActionResult> AddReader([FromBody] ReaderRequest readerRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = errors, receivedData = readerRequest });
            }

            var reader = new Reader
            {
                Name = readerRequest.Name,
                DateOfBirth = DateTime.Parse(readerRequest.DateOfBirth), // Parse YYYY-MM-DD
                Gender = readerRequest.Gender,
                PhoneNumber = readerRequest.PhoneNumber,
                Email = readerRequest.Email,
                Address = readerRequest.Address,
                EducationLevel = readerRequest.EducationLevel
            };

            // Tạo mã độc giả mới (DGxxxxx)
            var lastReader = await _context.Readers
                .OrderByDescending(r => r.ReaderCode)
                .FirstOrDefaultAsync();
            int newNumber = lastReader == null ? 0 : int.Parse(lastReader.ReaderCode.Replace("DG", "")) + 1;
            reader.ReaderCode = $"DG{newNumber.ToString().PadLeft(5, '0')}";

            reader.Role = "Reader";
            reader.Password = "defaultPassword";
            if (string.IsNullOrEmpty(reader.EducationLevel))
            {
                reader.EducationLevel = "Trung học";
            }

            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Độc giả đã được thêm thành công!",
                reader = new
                {
                    id = reader.ReaderCode,
                    name = reader.Name,
                    dob = reader.DateOfBirth.ToString("dd/MM/yyyy"),
                    gender = reader.Gender,
                    phone = reader.PhoneNumber,
                    email = reader.Email,
                    address = reader.Address,
                    education = reader.EducationLevel
                }
            });
        }

        // Sửa độc giả
        [HttpPut]
        public async Task<IActionResult> UpdateReader(string id, [FromBody] ReaderRequest updatedReaderRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ", errors = errors });
            }

            var reader = await _context.Readers.FirstOrDefaultAsync(r => r.ReaderCode == id);
            if (reader == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy độc giả!" });
            }

            // Cập nhật thông tin
            reader.Name = updatedReaderRequest.Name;
            reader.DateOfBirth = DateTime.Parse(updatedReaderRequest.DateOfBirth);
            reader.Gender = updatedReaderRequest.Gender;
            reader.PhoneNumber = updatedReaderRequest.PhoneNumber;
            reader.Email = updatedReaderRequest.Email;
            reader.Address = updatedReaderRequest.Address;
            reader.EducationLevel = updatedReaderRequest.EducationLevel;

            _context.Readers.Update(reader);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Thông tin độc giả đã được cập nhật!" });
        }

        // Xóa độc giả
        [HttpDelete]
        public async Task<IActionResult> DeleteReader(string id)
        {
            var reader = await _context.Readers.FirstOrDefaultAsync(r => r.ReaderCode == id);
            if (reader == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy độc giả!" });
            }

            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Độc giả đã được xóa!" });
        }


        
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Stats()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult LibraryCards()
        {
            return View();
        }

    }
}