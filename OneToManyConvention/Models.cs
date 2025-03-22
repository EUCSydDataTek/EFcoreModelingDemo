using Microsoft.EntityFrameworkCore;

namespace OneToManyConvention;

class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
}

// Principal (parent)
public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public ICollection<Post>? Posts { get; set; }   // Collection Navigation property
}

// Dependent (child)
public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    //public int BlogId { get; set; }           // Required foreign key property
    public Blog Blog { get; set; } = null!;     // Required Reference Navigation property to principal


    //public int? BlogId { get; set; }            // Optional foreign key property
    //public Blog? Blog { get; set; }             // Optional reference navigation to principal
}
