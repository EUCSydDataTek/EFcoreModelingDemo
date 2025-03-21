using CompleteModelDataSeeding;

BloggingContext context = new BloggingContext();

await using (context)
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();
    Console.WriteLine("Database deleted and created!");
}
