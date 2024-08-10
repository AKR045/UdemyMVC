using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UdemyMVC.Models
{
	public class UdemyDataBase:IdentityDbContext<ApplicationModel>
	{
        public  DbSet<User> Users{ get; set; }
        public  DbSet<Course> Courses{ get; set; }
        public  DbSet<Instructor> Instructors{ get; set; }
		public DbSet<Enrollment> Enrollments{ get; set; } 
		public DbSet<Category> Categories{ get; set; } 
		public DbSet<CategoryCourse> CategoryCourses { get; set; }
		public DbSet<Chapter> chapters { get; set; } 
		public DbSet<CourseRate>CourseRates { get; set; }	
		public DbSet<Topic> Topics { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=DESKTOP-SPBU74G;Database=UdemyDataBase;Trusted_Connection=True;Encrypt=False");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Enrollment>().HasKey(S => new { S.CourseID, S.UserID });//composite Key  
			modelBuilder.Entity<CategoryCourse>().HasKey(s => new { s.CourseID, s.CategoryID });
			modelBuilder.Entity<CourseRate>().HasKey(s => new { s.CourseID, s.UserID });
            base.OnModelCreating(modelBuilder);
		}
        public UdemyDataBase()
        {
            
        }
        public UdemyDataBase(DbContextOptions<UdemyDataBase> option):base(option)
        {
            
        }

    }
}
