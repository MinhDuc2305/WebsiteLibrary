using System.ComponentModel.DataAnnotations;

namespace WebsiteLibrary.Models.Entities
{
    public class Book
    {
        [Key] public Guid OriginalBookID { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalBookTitle { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Range(1, int.MaxValue)]
        public int PageCount { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(1800, 9999)]
        public int PublicationYear { get; set; }

        public Category Category { get; set; }

        public ICollection<BookCopy> BookCopies { get; set; }


    }
    public enum Category

    {

        [Display(Name = "Tài liệu học tập")]

        Tailieuhoctap,

        [Display(Name = "Tài liệu lịch sử")]

        Tailieulichsu,

        [Display(Name = "Sách phát triển bản thân")]

        Sachphattrienbanthan,

        [Display(Name = "Tiểu thuyết")]

        Tieuthuyet

    }
}
