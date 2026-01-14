using System.ComponentModel.DataAnnotations;

namespace LMS.Api.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(1, 500)]
    public int DurationInHours { get; set; }
}
