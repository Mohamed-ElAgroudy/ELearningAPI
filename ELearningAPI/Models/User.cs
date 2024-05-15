using System;
using System.Collections.Generic;

namespace ELearningAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public string? Area { get; set; }

    public string? MobileNumber { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<UserShoppingCartItem> UserShoppingCartItems { get; set; } = new List<UserShoppingCartItem>();
}
