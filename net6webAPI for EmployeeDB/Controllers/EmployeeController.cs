using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net6webAPI.Data;
using Microsoft.AspNetCore.Mvc;
using net6webAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace net6webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDBContext dbContext;

        private readonly IWebHostEnvironment _env;

        public EmployeeController(EmployeeDBContext dbContext, IWebHostEnvironment env)
        {
            this.dbContext = dbContext;

            this._env = env;
            
        }

        // GET: /<controller>/
        [HttpGet]

        public async Task<IActionResult> GetEmployee()
        {
            return Ok(await dbContext.Employee.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetEmployeeS([FromRoute] int id)
        {
            var employee = await dbContext.Employee.FindAsync(id);

            if (employee != null)
            {
        
                return Ok(employee);
            }

            return NotFound();
            
        }


        [HttpPost]

        public async Task<IActionResult> AddEmployee(AddEmployee addEmployee)
        {
            var employee = new Employee()
            {

                EmployeeName = addEmployee.EmployeeName,
                Department = addEmployee.Department,
                DateOfJoining = addEmployee.DateOfJoining,
                PhotoFileName = addEmployee.PhotoFileName,
            };

            await dbContext.Employee.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, UpdateEmployee updateEmployee)
        {
            var employee = await dbContext.Employee.FindAsync(id);

            if (employee != null)
            {
                employee.EmployeeName = updateEmployee.EmployeeName;
                employee.Department = updateEmployee.Department;
                employee.DateOfJoining = updateEmployee.DateOfJoining;
                employee.PhotoFileName = updateEmployee.PhotoFileName;

                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await dbContext.Employee.FindAsync(id);

            if (employee != null)
            {
                dbContext.Remove(employee);
                await dbContext.SaveChangesAsync();

                return Ok(employee);
            }

            return NotFound();
        }


         [HttpPost]
        [Route("SaveFile")]

        public async Task<ActionResult> SaveFile()
        {

            try
            {
                var httpRequest = Request.Form;

                var postedFile = httpRequest.Files[0];

                string filename = postedFile.FileName;

                string webRootPath = _env.WebRootPath;
                string relativePath = "/Images/";
                string absolutePath = webRootPath + relativePath + filename;



                using (FileStream stream = System.IO.File.Create(absolutePath))
                {
                    await postedFile.CopyToAsync(stream);

                }

                return Ok(new { fileName = filename });

            }

            catch (Exception)
            {
                return Ok("anonymous.png");
            }

        }


        [HttpGet]
        [Route("GetAllDepartmentNames")]

        public async Task<IActionResult> GetAllDepartmentNames()
        {
            var departmentNames = await dbContext.Department.Select(d => d.DepartmentName).ToListAsync();
            return Ok(departmentNames);
        }
    }


}


