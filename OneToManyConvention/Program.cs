using Microsoft.EntityFrameworkCore;
using OneToManyConvention;

BloggingContext context = new BloggingContext();

await using (context)
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    Console.WriteLine("Database deleted and created!");

    var blog = new Blog
    {
        Url = "https://itdata.net/blog/1",
        Posts = new List<Post>
        {
            new Post { Title = "Post1 title", Content = "Post1 content"},
            new Post { Title = "Post2 title", Content = "Post2 content"}
        }
    };

    context.Add<Blog>(blog);
    context.SaveChanges();

    Blog? firstBlog = context.Blogs.Include(b => b.Posts).FirstOrDefault();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Blog Id: {firstBlog!.BlogId} - Blog URL: {firstBlog.Url}");
    Console.ForegroundColor = ConsoleColor.Yellow;
    foreach (Post post in firstBlog.Posts!)
    {
        Console.WriteLine($"\tPost title: {post.Title}");
    }
    Console.ResetColor();
}