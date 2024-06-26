﻿using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Models.Files;
using ApplicationCore.Models.Auth;

namespace ApplicationCore.DataAccess;
public class DefaultContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
  
   public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
	{
      
   }
   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      if (Database.IsNpgsql())
      {
         var types = builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?));
         foreach (var property in types)
         {
            property.SetColumnType("timestamp without time zone");
         }
      }
   }
   public DbSet<ModifyRecord> ModifyRecords => Set<ModifyRecord>();
   public DbSet<Profiles> Profiles => Set<Profiles>();

   //Files
   public DbSet<Department> Departments => Set<Department>();
   public DbSet<JudgebookType> JudgebookTypes => Set<JudgebookType>();
   public DbSet<JudgebookFile> JudgebookFiles => Set<JudgebookFile>();


   #region Auth
   public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
	public DbSet<OAuth> OAuth => Set<OAuth>();
   public DbSet<AuthToken> AuthTokens => Set<AuthToken>();
   #endregion

   public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

}
