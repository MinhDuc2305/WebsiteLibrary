using Microsoft.EntityFrameworkCore;
using WebsiteLibrary.Models.Entities;

namespace WebsiteLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        //hàm dựng
        //tham số DbContextOptions -> lấy từ lớp kế thừa DbContext
        //=> sẽ tạo ra 1 đối tượng  ApplicationDbContext từ lớp DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }
        public DbSet<BorrowingSlip> BorrowingSlips { get; set; }
        public DbSet<BorrowingSlipDetail> BorrowingSlipDetails { get; set; }
        public DbSet<BookCopy> BookCopies { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CardRenewal> CardRenewals { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ImportReceipt> ImportReceipts { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }

        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<LoanRequestDetail> LoanRequestDetails { get; set; }

        public DbSet<ReturnSlip> ReturnSlips { get; set; }
        public DbSet<ReturnSlipDetail> ReturnSlipDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ImportDetail>()
                .HasKey(i => new { i.ImportReceiptId, i.OriginalBookId });

            modelBuilder.Entity<ImportDetail>()
                .HasOne(i => i.ImportReceipt)
                .WithMany()
                .HasForeignKey(i => i.ImportReceiptFK)
                .OnDelete(DeleteBehavior.NoAction); // Thay CASCADE thành NO ACTION

            modelBuilder.Entity<ImportDetail>()
                .HasOne(i => i.Book)
                .WithMany()
                .HasForeignKey(i => i.OriginalBookFK)
                .OnDelete(DeleteBehavior.NoAction); // Tùy chọn, nếu cần

            // LoanRequestDetail
            modelBuilder.Entity<LoanRequestDetail>()
        .HasKey(l => new { l.RequestId, l.BookCopyId });
            modelBuilder.Entity<LoanRequestDetail>()
                .HasOne(l => l.LoanRequest)
                .WithMany()
                .HasForeignKey(l => l.RequestFK)
                .OnDelete(DeleteBehavior.NoAction); // Tránh cascade xung đột
            modelBuilder.Entity<LoanRequestDetail>()
                .HasOne(l => l.BookCopy)
                .WithMany()
                .HasForeignKey(l => l.BookCopyFK)
                .OnDelete(DeleteBehavior.NoAction);

            // BorrowingSlipDetail
            modelBuilder.Entity<BorrowingSlipDetail>()
        .HasKey(b => new { b.BorrowingSlipId, b.BookCopyId });
            modelBuilder.Entity<BorrowingSlipDetail>()
                .HasOne(b => b.BorrowingSlip)
                .WithMany()
                .HasForeignKey(b => b.BorrowingSlipFK)
                .OnDelete(DeleteBehavior.NoAction); // Tránh cascade xung đột
            modelBuilder.Entity<BorrowingSlipDetail>()
                .HasOne(b => b.BookCopy)
                .WithMany()
                .HasForeignKey(b => b.BookCopyFK)
                .OnDelete(DeleteBehavior.NoAction);

            // ReturnSlipDetail
            modelBuilder.Entity<ReturnSlipDetail>()
        .HasKey(r => new { r.ReturnSlipId, r.BookId });
            modelBuilder.Entity<ReturnSlipDetail>()
                .HasOne(r => r.ReturnSlip)
                .WithMany()
                .HasForeignKey(r => r.ReturnSlipFK)
                .OnDelete(DeleteBehavior.NoAction); // Tránh cascade xung đột
            modelBuilder.Entity<ReturnSlipDetail>()
                .HasOne(r => r.BookCopy)
                .WithMany()
                .HasForeignKey(r => r.BookCopyFK)
                .OnDelete(DeleteBehavior.NoAction);

            // Cấu hình inheritance cho Reader và Librarian
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("Role")
                .HasValue<Reader>("Reader")
                .HasValue<Librarian>("Admin");
        }

    }
}
