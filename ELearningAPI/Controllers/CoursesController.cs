using ELearningAPI.Data;
using ELearningAPI.DTOS;
using ELearningAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ElearningReusableContext _context;
        public CoursesController(ElearningReusableContext context)
        {
            this._context = context;
        }
        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<List<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseDetails(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
            
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(CourseDto course)
        {
            try
            {
                if (await _context.Courses.AnyAsync(c => c.Title == course.Title))
                {
                    return Conflict("Course Title already exists");
                }
                var coursetba = new Course
                {
                    Title = course.Title,
                    Price = course.Price,
                    Description = course.Description,
                    Category = course.Category,
                };
                _context.Courses.Add(coursetba);
                await _context.SaveChangesAsync();

                return Ok("Course added successfully");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpPut("EditCourse")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto model)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return NotFound("Course not found");
                }

                course.Title = model.Title;
                course.Description = model.Description;
                course.Category = model.Category;
                course.Price = model.Price;

                await _context.SaveChangesAsync();

                return Ok("Course updated successfully");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int courseid)
        {
            var coursetbd = await _context.Courses.FindAsync(courseid);
            if (coursetbd == null)
            {
                return Conflict("No course with this Id Exists");
            }
            _context.Courses.Remove(coursetbd);
            await _context.SaveChangesAsync();
            return Ok("Course Removed successfully");
        }
    }
}
