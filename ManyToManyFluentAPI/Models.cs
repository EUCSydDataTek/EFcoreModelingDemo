using Microsoft.EntityFrameworkCore;

namespace ManyToManyFluentAPI;

class BloggingContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
        .HasMany(e => e.Tags)
        .WithMany(e => e.Posts)
        .UsingEntity("PostsToTagsJoinTable");
    }
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public List<Tag>? Tags { get; set; }
}


public class Tag
{
    public string? TagId { get; set; }

    public List<Post>? Posts { get; set; }
}

public class PostsToTagsJoinTable
{
    public int PostId { get; set; }
    public Post? Post { get; set; }

    public string? TagId { get; set; }
    public Tag? Tag { get; set; }
}
