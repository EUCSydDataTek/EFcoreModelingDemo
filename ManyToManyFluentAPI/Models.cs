using Microsoft.EntityFrameworkCore;

namespace ManyToManyFluentAPI;

class BloggingContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostTag>()
                 .HasKey(t => new { t.PostId, t.TagId });

        modelBuilder.Entity<PostTag>()
        .HasOne(pt => pt.Post)
        .WithMany(p => p.PostTags)
        .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);
    }
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public List<PostTag>? PostTags { get; set; }
}

public class PostTag
{
    public int PostId { get; set; }
    public Post? Post { get; set; }

    public string? TagId { get; set; }
    public Tag? Tag { get; set; }
}

public class Tag
{
    public string? TagId { get; set; }

    public List<PostTag>? PostTags { get; set; }
}
