using Microsoft.AspNetCore.Mvc;
using LMS.Api.Data;
using LMS.Api.Models;

namespace LMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetCourses()
    {
        var courses = _context.Courses.ToList();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public IActionResult GetCourseById(int id)
    {
        var course = _context.Courses.Find(id);

        if (course == null)
            return NotFound();

        return Ok(course);
    }

    [HttpPost]
    public IActionResult AddCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
        return Ok(course);
    }

    // ✅ PHASE 4 — STEP 7 (UPDATE)
    [HttpPut("{id}")]
    public IActionResult UpdateCourse(int id, Course updatedCourse)
    {
        var course = _context.Courses.Find(id);

        if (course == null)
            return NotFound();

        course.Title = updatedCourse.Title;
        course.Description = updatedCourse.Description;
        course.DurationInHours = updatedCourse.DurationInHours;

        _context.SaveChanges();
        return Ok(course);
    }
    [HttpDelete("{id}")]
public IActionResult DeleteCourse(int id)
{
    var course = _context.Courses.Find(id);

    if (course == null)
        return NotFound();

    _context.Courses.Remove(course);
    _context.SaveChanges();

    return NoContent();
}

}
