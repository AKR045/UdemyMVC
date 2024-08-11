using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyMVC.Models;

namespace UdemyMVC.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(UdemyDataBase context) : base(context) { }

        public async Task<IEnumerable<Course>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            return await _dbSet.Include(c => c.CourseRates).Where(c => c.InstructorID == instructorId).ToListAsync();
        }
        public async Task<Course?> GetCoursesByCourseIdAsync(int courseId)
        {
            return await _dbSet.Include(c => c.Instructor).ThenInclude(c => c.User)
                .Include(c => c.CategoryCourses).ThenInclude(c => c.Category)
                .Include(c => c.CourseRates)
                .Include(c=>c.Chapters).ThenInclude(c=>c.Topics)
                .Include(c => c.Enrollment)
                .FirstOrDefaultAsync(c => c.ID == courseId);
        }


    



    }
}
