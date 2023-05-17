using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using net6webAPI.Data;
using Microsoft.AspNetCore.Mvc;
using net6webAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace net6webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DepartmentController : ControllerBase
    {
        private readonly EmployeeDBContext dbContext;

        public DepartmentController(EmployeeDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /<controller>/
        [HttpGet]

        public async Task<IActionResult> GetDepartment()
        {
            return Ok(await dbContext.Department.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]

        public async Task<IActionResult> GetDepartmentS([FromRoute] int id)
        {
            var department = await dbContext.Department.FindAsync(id);

            if (department != null)
            {

                return Ok(department);
            }

            return NotFound();

        }


        [HttpPost]

        public async Task<IActionResult> AddDepartment(AddDepartment addDepartment)
        {
            var department = new Department()
            {

                DepartmentName = addDepartment.DepartmentName,
       
            };

            await dbContext.Department.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return Ok(department);
        }

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateDepartment([FromRoute] int id, UpdateDepartment updateDepartment)
        {
            var department = await dbContext.Department.FindAsync(id);

            if (department != null)
            {
                department.DepartmentName = updateDepartment.DepartmentName;
               

                await dbContext.SaveChangesAsync();

                return Ok(department);
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            var department = await dbContext.Department.FindAsync(id);

            if (department != null)
            {
                dbContext.Remove(department);
                await dbContext.SaveChangesAsync();

                return Ok(department);
            }

            return NotFound();
        }


    }
}
