using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UdemyMVC.Models;
using UdemyMVC.Repositories;

namespace UdemyMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews(); 
			builder.Services.AddScoped<ICourseRepository,CourseRepository>();	
			//Inject DataBase Connection 
			builder.Services.AddDbContext<UdemyDataBase>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("cs")); 
			});
			//Inject UserManager,IdentityRole,SignInManager
			builder.Services.AddIdentity<ApplicationModel, IdentityRole>()
				.AddEntityFrameworkStores<UdemyDataBase>();
			//inject Repositories
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting(); 
			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Account}/{action=Login}/{id?}");

			app.Run();
		}
	}
}
