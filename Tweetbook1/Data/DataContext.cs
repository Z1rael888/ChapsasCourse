using Microsoft.EntityFrameworkCore;
using Tweetbook1.Domain;

namespace Tweetbook1.Data;

public class DataContext :DbContext 
{
    public DataContext(DbContextOptions<DataContext> options):
        base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
}