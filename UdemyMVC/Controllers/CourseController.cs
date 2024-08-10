using Microsoft.AspNetCore.Mvc;
using UdemyMVC.Models;
using UdemyMVC.Repositories;
using UdemyMVC.ViewModels;

namespace UdemyMVC.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepository courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        //async methoud should be awaited =>  use async Task 

        public async Task<IActionResult> CourseDetailes(int id)
        {
            Course course = await courseRepository.GetCoursesByCourseIdAsync(id);
            int instId = course.InstructorID;
            IEnumerable<Course> courses = await courseRepository.GetCoursesByInstructorIdAsync(instId);

            if (course == null)
            {
                return NotFound();
            }
            decimal sumRateAll = course.CourseRates.Select(c => c.Rate).Sum();
            int countRateAll = course.CourseRates.Select(c => c.Rate).Count();

            decimal sumRate = course.CourseRates.Where(c=>c.CourseID==id).Select(c => c.Rate).Sum();
            int countRate = course.CourseRates.Where(c => c.CourseID == id).Select(c => c.Rate).Count();
            CourseDetailsVM courseVM = new CourseDetailsVM();
            courseVM.Id = course.ID;
            courseVM.CourseName = course.CourseName;
            courseVM.Title = course.Title;
            courseVM.Description = course.Description;
            courseVM.CourseImg = course.CourseImage;
            courseVM.Price = course.Price;
            //courseVM.Chapters = course.Chapters.ToList();
            //courseVM.Topics = course.Chapters.Select(c=>c.Topics).ToString();
            courseVM.Rating = countRate > 0 ? (sumRate / countRate).ToString() : "1";
            courseVM.NumberOfRating = countRate;
            courseVM.NumberOfStudent = course.Enrollment.Count();
            courseVM.Instructor = course.Instructor.User.FullName;
            courseVM.Category = course.CategoryCourses.Select(c => c.Category.CategoryName).ToList();

            /* Instructor Data */
            courseVM.AllRateCount=countRateAll;
            courseVM.InstructorIamge=course.Instructor.User.Image;
            courseVM.InstructorAllCourses = courses.ToList();
            courseVM.InstructorAllCoursesNumber= courses.Count();
            courseVM.InstructorAllStudentsNumber= courses.Select(c=>c.Enrollment).Distinct().Count();
            courseVM.AllCourseRate = countRateAll > 0 ? (sumRateAll / countRateAll).ToString():"1";
            
            return View("CourseDetailes", courseVM);
        }
    }
}
