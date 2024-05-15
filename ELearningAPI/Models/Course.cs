using System;
using System.Collections.Generic;

namespace ELearningAPI.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<UserShoppingCartItem> UserShoppingCartItems { get; set; } = new List<UserShoppingCartItem>();
}
