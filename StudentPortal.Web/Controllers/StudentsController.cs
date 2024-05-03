using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
    public class StudentsController : Controller 
        //first we will inject the dbcontext class inside the controller
        //we will make use of constructor injection
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Add(AddStudentViewModel viewModel) 

        /*here we are able to get the information hovring on viewmodel now we are able to use the Dbcontext file  */
        {
            var studnt = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,
            };
            await dbContext.Students.AddAsync(studnt);
            await dbContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task <IActionResult> List()
        {
           var students = await dbContext.Students.ToListAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(); // Return a 404 Not Found if student with the given ID is not found
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student); // Return the view with validation errors if model state is invalid
            }

            var existingStudent = await dbContext.Students.FindAsync(student.Id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = student.Name;
            existingStudent.Email = student.Email;
            existingStudent.Phone = student.Phone;
            existingStudent.Subscribed = student.Subscribed;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(List)); // Redirect to the list action after successful update
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }




    }
}
