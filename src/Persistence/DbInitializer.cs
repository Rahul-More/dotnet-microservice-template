using Domain.Entities;

namespace Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Name = "Seed User",
                Email = "seed@example.com"
            });
            await context.SaveChangesAsync();
        }
    }
}