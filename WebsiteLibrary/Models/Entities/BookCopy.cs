using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteLibrary.Models.Entities
{
    public class BookCopy
    {
        [Key]

        public Guid BookID { get; set; }

        [Required]
        [ForeignKey("Book")]
        public Guid OriginalBookID { get; set; }

        public Condition Condition { get; set; }

        public DateTime ImportDate { get; set; }

        public Book Book { get; set; }


    }

    public enum Condition
    {
        [Display(Name = "Nguyên vẹn")]
        Nguyên_vẹn,
        [Display(Name = "Hỏng")]
        Hỏng,
        [Display(Name = "Mất")]
        Mất
    }
}
