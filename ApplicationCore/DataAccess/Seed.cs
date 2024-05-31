using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ApplicationCore.Models;
using ApplicationCore.Consts;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models.Files;

namespace ApplicationCore.DataAccess;

public static class SeedData
{
	
	static string DevRoleName = AppRoles.Dev.ToString();
	static string BossRoleName = AppRoles.Boss.ToString();
   static string ITRoleName = AppRoles.IT.ToString();
   static string RecorderRoleName = AppRoles.Recorder.ToString();
   static string ClerkRoleName = AppRoles.Clerk.ToString();
   static string FilesRoleName = AppRoles.Files.ToString();

   public static async Task EnsureSeedData(IServiceProvider serviceProvider, ConfigurationManager Configuration)
	{
		string adminEmail = Configuration[$"{SettingsKeys.Admin}:Email"] ?? "";
		string adminPhone = Configuration[$"{SettingsKeys.Admin}:Phone"] ?? "";
		string adminName = Configuration[$"{SettingsKeys.Admin}:Name"] ?? "";

		if(String.IsNullOrEmpty(adminEmail) || String.IsNullOrEmpty(adminPhone))
		{
			throw new Exception("Failed to SeedData. Empty Admin Email/Phone.");
		}
		if(String.IsNullOrEmpty(adminName))
		{
			throw new Exception("Failed to SeedData. Empty Admin Name.");
		}

		Console.WriteLine("Seeding database...");

		var context = serviceProvider.GetRequiredService<DefaultContext>();
	   context.Database.EnsureCreated();
		using (var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>())
		{
			await SeedRoles(roleManager);
		}

		if(!String.IsNullOrEmpty(adminEmail)) {
			using (var userManager = serviceProvider.GetRequiredService<UserManager<User>>())
			{
				var user = new User
				{
					Email = adminEmail,			
					UserName = adminEmail,
					Name = adminName,
					PhoneNumber = adminPhone,
					EmailConfirmed = true,
					SecurityStamp = Guid.NewGuid().ToString(),
					Active = true
				};
				await CreateUserIfNotExist(userManager, user, new List<string>() { DevRoleName });
			}
		}
      await SeedJudgebookTypes(context);
      Console.WriteLine("Done seeding database.");
	}

	static async Task SeedRoles(RoleManager<Role> roleManager)
	{
		var roles = new List<Role> 
		{ 
			new Role { Name = DevRoleName, Title = "開發者" },
         new Role { Name = BossRoleName, Title = "老闆" },
         new Role { Name = ITRoleName, Title = "資訊人員" },
         new Role { Name = RecorderRoleName, Title = "錄事" },
         new Role { Name = ClerkRoleName, Title = "書記官" },
         new Role { Name = FilesRoleName, Title = "檔案管理員" }
      };
		foreach (var item in roles) await AddRoleIfNotExist(roleManager, item);
	}
	static async Task AddRoleIfNotExist(RoleManager<Role> roleManager, Role role)
	{
		var existingRole = await roleManager.FindByNameAsync(role.Name!);
		if (existingRole == null) await roleManager.CreateAsync(role);
		else
		{
         existingRole.Title = role.Title;
			await roleManager.UpdateAsync(existingRole);
      } 

   }
	static async Task CreateUserIfNotExist(UserManager<User> userManager, User newUser, IList<string>? roles = null)
	{
		var user = await userManager.FindByEmailAsync(newUser.Email!);
		if (user == null)
		{
			var result = await userManager.CreateAsync(newUser);

			if (roles!.HasItems())
			{
				await userManager.AddToRolesAsync(newUser, roles!);
			}
		}
		else
		{
			user.PhoneNumber = newUser.PhoneNumber;
			user.Name = newUser.Name;
			await userManager.UpdateAsync(user);
			if (roles!.HasItems())
			{
				foreach (var role in roles!)
				{
					bool hasRole = await userManager.IsInRoleAsync(user, role);
					if (!hasRole) await userManager.AddToRoleAsync(user, role);
				}
			}

		}
	}

   static async Task SeedJudgebookTypes(DefaultContext context)
   {
      var types = new List<JudgebookType>
      {
         new JudgebookType() { Key = "JU", Title = "判決", Order = 0 },
         new JudgebookType() { Key = "RU", Title = "裁定", Order = 1 },
         
         new JudgebookType() { Key = "ST", Title = "和解筆錄",Order = 2 },
         new JudgebookType() { Key = "MT", Title = "調解筆錄",Order = 3 },

         new JudgebookType() { Key = "JUC", Title = "更正判決", Order = 4 },
         new JudgebookType() { Key = "RUC", Title = "更正裁定", Order = 5 }
      };

		foreach (var item in types)
		{
			await AddJudgebookTypeIfNotExist(context, item);
      }
      context.SaveChanges();
   }
   static async Task AddJudgebookTypeIfNotExist(DefaultContext context, JudgebookType type)
	{
		if (context.JudgebookTypes.Count() == 0)
		{
			context.JudgebookTypes.Add(type);
			return;
		}
		var exist = await context.JudgebookTypes.FirstOrDefaultAsync(x => x.Title == type.Title);
		if (exist == null) context.JudgebookTypes.Add(type);
		else
		{
         exist.Key = type.Key;
         exist.Order = type.Order;
      } 
	}
}