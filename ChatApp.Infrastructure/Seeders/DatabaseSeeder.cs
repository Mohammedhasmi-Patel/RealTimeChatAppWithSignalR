using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Infrastructure.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {

        try
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await RoleSeeder.SeedAysnc(roleManager);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
