using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyMVC.Models;

namespace UdemyMVC.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {

        Task<IEnumerable<Course?>> GetCoursesByInstructorIdAsync(int instructorId);
        Task<Course?> GetCoursesByCourseIdAsync(int courseId);
    }
}
