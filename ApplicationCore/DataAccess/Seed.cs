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
   static string ChiefClerkRoleName = AppRoles.ChiefClerk.ToString();
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
      await SeedDepartments(context);
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
         new Role { Name = ChiefClerkRoleName, Title = "紀錄科長" },
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
	static async Task SeedDepartments(DefaultContext context)
	{
		var departments = new List<Department>
		{
			new Department() { Key = "01", Title = "博", Order = 1, CourtTypes = "H,V" },
			new Department() { Key = "02", Title = "學", Order = 2, CourtTypes = "H,V" },

			new Department() { Key = "03", Title = "以",Order = 3, CourtTypes = "H,V" },
			new Department() { Key = "04", Title = "於",Order = 4, CourtTypes = "H,V" },

			new Department() { Key = "05", Title = "禮", Order = 5, CourtTypes = "H,V" },
			new Department() { Key = "06", Title = "文", Order = 6, CourtTypes = "H,V" },


			new Department() { Key = "07", Title = "慎", Order = 7, CourtTypes = "H,V" },
			new Department() { Key = "08", Title = "勤", Order = 8, CourtTypes = "H,V" },

			new Department() { Key = "09", Title = "約",Order = 9, CourtTypes = "H,V" },
			new Department() { Key = "10", Title = "為",Order = 10, CourtTypes = "H,V" },

			new Department() { Key = "11", Title = "義", Order = 11, CourtTypes = "H,V" },
			new Department() { Key = "12", Title = "仁", Order = 12, CourtTypes = "H,V" },

			new Department() { Key = "13", Title = "信", Order = 13, CourtTypes = "H,V" },
			new Department() { Key = "14", Title = "敬", Order = 14, CourtTypes = "H,V" },

			new Department() { Key = "15", Title = "樂", Order = 15, CourtTypes = "V" },
			new Department() { Key = "16", Title = "孝", Order = 16, CourtTypes = "V" },
			new Department() { Key = "17", Title = "賢", Order = 17, CourtTypes = "V" },
			new Department() { Key = "18", Title = "德", Order = 18, CourtTypes = "V" }
		};
      foreach (var item in departments)
      {
         await AddDepartmentsIfNotExist(context, item);
      }
      context.SaveChanges();
   }
   static async Task AddDepartmentsIfNotExist(DefaultContext context, Department department)
   {
      if (context.Departments.Count() == 0)
      {
         context.Departments.Add(department);
         return;
      }
      var exist = await context.Departments.FirstOrDefaultAsync(x => x.Key == department.Key);
      if (exist == null) context.Departments.Add(department);
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