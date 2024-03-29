using Diskusjonsforum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thread = Diskusjonsforum.Models.Thread;

namespace diskusjonsforum.DAL;

public class ThreadDbContext : IdentityDbContext
{
	public ThreadDbContext(DbContextOptions<ThreadDbContext> options) : base(options)
	{
		//Database.EnsureDeleted();
		//Database.EnsureCreated();
		
		//Lazy loading (might not work properly) 
		ChangeTracker.LazyLoadingEnabled = true;
		//(Should) enable lazy loading of related entities. This means that entities related to the class you're accessing (foreign keys) are loaded when needed rather than when 
		//instructed to in the controller. E.g. when displaying a comment using comment controller, the associated Thread that's displayed is also loaded automatically 
	}

    public DbSet<Thread> Threads { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
	    base.OnModelCreating(builder);
	    
	    builder.Entity<Category>().HasKey(c => c.CategoryName);

	    builder.Entity<Thread>()
		    .HasOne(t => t.User)
		    .WithMany()
		    .HasForeignKey(t => t.UserId);
	    builder.Entity<Thread>()
		    .HasOne(t => t.Category)
		    .WithMany(c => c.ThreadsInCategory)
		    .HasForeignKey(t => t.CategoryName);
	    builder.Entity<Comment>()
		    .HasOne(t => t.User)
		    .WithMany()
		    .HasForeignKey(t => t.UserId);
    }
}


