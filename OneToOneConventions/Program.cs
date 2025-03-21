using Microsoft.EntityFrameworkCore;
using OneToOneConventions;

BloggingContext context = new BloggingContext();

await using (context)
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    Console.WriteLine("Database deleted and created!");

    var blog = new Blog
    {
        Url = "https://itdata.net/blog/1",
        BlogImage = new BlogImage
        {
            Caption = "Image Blog 1"
        }
    };
    context.Add<Blog>(blog);
    context.SaveChanges();

    Blog? firstBlog = context.Blogs.Include(b => b.BlogImage).FirstOrDefault();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Blog URL: {firstBlog!.Url} - Image caption: {firstBlog.BlogImage?.Caption}");
    Console.ResetColor();
}