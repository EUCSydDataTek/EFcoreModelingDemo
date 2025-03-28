﻿using Microsoft.EntityFrameworkCore;

namespace OneToOneFluentAPI;

class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasOne(p => p.BlogImage)
            .WithOne(i => i.Blog)
            .HasForeignKey<BlogImage>(b => b.BlogForeignKey)
            .IsRequired();
    }
}

public class Blog
{
    public int BlogId { get; set; }
    public string? Url { get; set; }

    public BlogImage? BlogImage { get; set; }
}

public class BlogImage
{
    public int BlogImageId { get; set; }
    public byte[]? Image { get; set; }
    public string? Caption { get; set; }

    //public int BlogId { get; set; }             // FK name matches PK
    public int BlogForeignKey { get; set; }       // FK, name don't matches PK
    public Blog Blog { get; set; } = null!;
}