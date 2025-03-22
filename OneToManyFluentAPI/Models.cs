using Microsoft.EntityFrameworkCore;

namespace OneToManyFluentAPI;

class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasMany(e => e.Posts)
            .WithOne(e => e.Blog)
            .HasForeignKey(e => e.FKBlogId)
            .IsRequired();
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Post>()
    //        .HasOne(e => e.Blog)
    //        .WithMany(e => e.Posts)
    //        .HasForeignKey(e => e.FKBlogId)
    //        .IsRequired();
    //}
}

// Principal (parent)
public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }
    public ICollection<Post>? Posts { get; set; }
}

// Dependent (child)
public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int FKBlogId { get; set; }       // Required foreign key property with unconventional name
    public Blog Blog { get; set; } = null!; // Required reference navigation to principal
}
