using Microsoft.EntityFrameworkCore;

namespace ManyToManyConventions;

class BloggingContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite key
        //modelBuilder.Entity<PostTag>()
        //    .HasKey(t => new { t.PostId, t.TagId });
    }
}

public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    //public ICollection <PostTag> PostTags { get; set; }

    public ICollection<Tag>? Tags { get; set; }
}

//public class PostTag
//{
//    public int PostId { get; set; }
//    public Post Post { get; set; }

//    public string TagId { get; set; }
//    public Tag Tag { get; set; }
//}

public class Tag
{
    public string? TagId { get; set; }

    //public ICollection<PostTag> PostTags { get; set; }
    public ICollection<Post>? Posts { get; set; }
}
