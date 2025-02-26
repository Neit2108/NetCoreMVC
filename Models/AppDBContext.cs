
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace NetCoreMVC.Models;

public class AppDbContext : DbContext{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // foreach(var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //     var tableName = entityType.GetTableName();
        //     if(tableName.StartsWith("AspNet"))
        //     {
        //         entityType.SetTableName(tableName.Substring(6));
        //     } 
        // }
    }

    public DbSet<Contact> Contacts { get; set; }
}