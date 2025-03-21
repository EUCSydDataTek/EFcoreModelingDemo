﻿using Microsoft.EntityFrameworkCore;

namespace OneToOneConventions;

class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
        .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = BloggingDb; Trusted_Connection = True; ")
        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
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

        public int BlogId { get; set; }     // FK
        public Blog? Blog { get; set; }
    }

