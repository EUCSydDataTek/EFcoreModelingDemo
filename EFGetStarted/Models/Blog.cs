using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EFGetStarted.Models;

//[Index(nameof(Url))]
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public ICollection<Post> Posts { get; } = [];
}
