using ChatApp.Domain.Enum;
using ChatApp.Infrastructure.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Seeders;

public static class RoleSeeder
{
    public static async Task SeedAysnc(RoleManager<ApplicationRole> roleManager)
    {
        var roleNames = Enum.GetNames<UserRoleEnum>();
        var existingRoles = await roleManager.Roles
            .Where(x => x.Name != null)
            .Select(x => x.Name!)
            .ToListAsync();

        var rolesToAdd = roleNames
            .Except(existingRoles)
            .Where(role => !string.IsNullOrWhiteSpace(role))
            .Select(role => new ApplicationRole
            {
                Name = role,
                NormalizedName = role.ToUpperInvariant(),
                Description = role,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            })
            .ToList();

        foreach (var role in rolesToAdd)
        {
            await roleManager.CreateAsync(role);
        }
    }
}
