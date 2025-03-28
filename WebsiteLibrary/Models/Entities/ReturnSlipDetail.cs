using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebsiteLibrary.Models.Entities
{
    public class ReturnSlipDetail

    {

        [Key, Column(Order = 0)]
        public Guid ReturnSlipId { get; set; } // Khóa chính

        [Key, Column(Order = 1)]
        public Guid BookId { get; set; } // Khóa chính

        [ForeignKey("ReturnSlip")]
        public Guid ReturnSlipFK { get; set; } // Khóa ngoại riêng

        [ForeignKey("BookCopy")]
        public Guid BookCopyFK { get; set; } // Khóa ngoại riêng
        public Condition Condition { get; set; }
        public ReturnSlip ReturnSlip { get; set; }

        public BookCopy BookCopy { get; set; }

    }
}
