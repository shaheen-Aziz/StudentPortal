using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext // ApplicationDbContext is a bridge between our application and sql server 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        public DbSet<Student> Students { get; set; }  // 'DbSet' a collection of perticular type and here perticular type is student and table name here is Students
    }
}
