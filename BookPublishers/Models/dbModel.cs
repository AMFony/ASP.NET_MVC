using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookPublishers.Models
{
    public enum genere { Novel = 1, Fiction, Islamic }
    public class Publisher
    {
        public int PublisherId { get; set; }
        [Required, Display(Name = "Publisher Name"), StringLength(50)]
        public string publisherName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Stablished Date"), DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime stablishedDate { get; set; }
        public virtual ICollection<Editor> Editors { get; set; } = new List<Editor>();
        public virtual ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required, Display(Name = "Title"), StringLength(50)]
        public string bookName { get; set; }
        [Required, Display(Name = "Page")]
        public int page { get; set; }
        [EnumDataType(typeof(genere)), Display(Name = "Genere")]
        public genere genere { get; set; }
        [Display(Name = "Is available?")]
        public bool isAvailable { get; set; }
        
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual ICollection<BookPublisher> BookPublishers { get; set; } = new List<BookPublisher>();
    }
    public class Author
    {
        public int AuthorId { get; set; }
        [Required, Display(Name = "Author name"), StringLength(50)]
        public string AuthorName { get; set; }
        [Required, Display(Name = "Mobile No"), StringLength(20)]
        public string MobileNo { get; set; }
        [Required, StringLength(150)]
        public string picture { get; set; }
        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        
    }
    public class Editor
    {
        public int EditorId { get; set; }
        [Required, Display(Name = "Editor name"), StringLength(50)]
        public string EditorName { get; set; }
        [Required, Display(Name = "Mobile No"), StringLength(20)]
        public string MobileNo { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
    public class BookPublisher
    {
        [Key, Column(Order = 0), ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        [Key, Column(Order = 1), ForeignKey("Book")]
        public int BookId { get; set; }
        public Publisher Publisher { get; set; }
        public Book Book { get; set; }
        
    }
    public class PublisherDbContext : DbContext
    {
        public PublisherDbContext()
        {
            Database.SetInitializer(new PublisherDbInitializer());
        }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<BookPublisher> BookPublishers { get; set; }
    }
    public class PublisherDbInitializer : DropCreateDatabaseIfModelChanges<PublisherDbContext>
    {
        protected override void Seed(PublisherDbContext context)
        {
            Publisher p = new Publisher { publisherName = "Gurdian", stablishedDate = DateTime.Now.AddDays(-5 * 30) };
            Book b = new Book { bookName = "Secret of Jionism", page = 298, genere = genere.Islamic, isAvailable = true };
            p.Editors.Add(new Editor { EditorName = "Sakib", MobileNo = "01615000000" });
            b.Authors.Add(new Author { AuthorName = "Mahidul", MobileNo = "01615000000", picture = "1.jpg" });
            BookPublisher bp = new BookPublisher { Publisher = p, Book = b };
            context.Publishers.Add(p);
            context.Books.Add(b);
            context.BookPublishers.Add(bp);
            context.SaveChanges();
        }
    }
}