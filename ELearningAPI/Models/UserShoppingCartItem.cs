using System;
using System.Collections.Generic;

namespace ELearningAPI.Models;

public partial class UserShoppingCartItem
{
    public int UserShoppingCartItemId { get; set; }

    public string UserEmail { get; set; } = null!;

    public int CourseId { get; set; }

    public int Quantity { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User UserEmailNavigation { get; set; } = null!;
}
