using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ProjectContext : DbContext
{
    public DbSet<BallPark> BallParks { get; set; }

    public ProjectContext()
    {
    }

    public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (options.IsConfigured)
        {
            return;
        }

        IConfiguration config = new ConfigurationBuilder()
            .AddUserSecrets<ProjectContext>()
            .Build();

        options.UseSqlServer(config["ConnectionStrings:Project"]);
    }
}
