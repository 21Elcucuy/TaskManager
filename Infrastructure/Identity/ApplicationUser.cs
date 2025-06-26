using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser: IdentityUser
{
    public String FullName { get; set; }
    public ICollection<TaskItem> TaskItems { get; set; }
}