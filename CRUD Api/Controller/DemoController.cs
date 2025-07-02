using System.Security.Cryptography.Xml;
using CRUD_Api.DB;
using CRUD_Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CRUD_Api.Controller
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
 
    public class DemoController : ControllerBase
    {
        private readonly dbcontext _dbcontext;

        public DemoController(dbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] StudentDTO cd)
        {
            if (cd.Password != cd.ConfirmPassword)
                return BadRequest("Password and Confirm Password do not match.");

            var department = await _dbcontext.Departments
                .FirstOrDefaultAsync(d => d.DepartmentID == cd.DepartmentID);

            if (department == null)
                return BadRequest("Department not found.");

            var add = new crudclass
            {
                Name = cd.Name,
                Fathername = cd.Fathername,
                Dateofbirth = cd.Dateofbirth,
                Gender = cd.Gender,
                DepartmentID = cd.DepartmentID
            };

            var passwordHasher = new PasswordHasher<crudclass>();
            add.Password = passwordHasher.HashPassword(add, cd.Password); // ✅ fixed this line

            _dbcontext.Add(add);
            await _dbcontext.SaveChangesAsync();

            return Ok("Student Added Successfully");
        }



        [HttpGet("ShowStudentData")]
        //[Authorize]
        public async Task<IActionResult> ShowStudentData()
        {
            var data = await _dbcontext.Data.ToListAsync();


            if (data == null || !data.Any())
            {
                return NotFound("No students found.");
            }

            var studentDTOs = data.Select(n => new StudentDTO
            {
                Id = n.Id,
                Name = n.Name,
                Fathername = n.Fathername,
                Dateofbirth = n.Dateofbirth,
                Gender = n.Gender,
                DepartmentID = n.DepartmentID, // only if StudentDTO has this property
                //DepartmentName = n.Department?.DepartmentName ?? ""
            }).ToList();

            return Ok(studentDTOs);
        }

        [HttpDelete("DeleteStudentData/{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var data = await _dbcontext.Data.FirstOrDefaultAsync(n => n.Id == id);

            _dbcontext.Data.Remove(data);
            await _dbcontext.SaveChangesAsync();

            return Ok("Student Deleted Successfully");
        }
        [HttpPut("UpdateStudetData/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateStudentData(int id, [FromBody]StudentDTO put)
        {
            if (put == null)
            {
                return BadRequest("NOt Found");
            }
            var data =  _dbcontext.Data.FirstOrDefault(n =>n.Id == id);
            if(data == null)
            {
                return BadRequest("Data not found");
            }
            data.Name = put.Name;
            data.Fathername = put.Fathername;
            data.Dateofbirth = put.Dateofbirth;
            data.Gender = put.Gender;
            data.DepartmentID = put.DepartmentID;

            _dbcontext.Data.Update(data);
            await _dbcontext.SaveChangesAsync();

            return Ok("Student Data is Updated");
        }
        [HttpGet("ShowDataById/{id}")]
        //[Authorize]
        public async Task <IActionResult> showData(int id)
        {
             var data = _dbcontext.Data.FirstOrDefault(n=>n.Id== id);
            if(data == null)
            {
                return BadRequest("student not found");
            }
            return Ok(data);
        }

    }
}
