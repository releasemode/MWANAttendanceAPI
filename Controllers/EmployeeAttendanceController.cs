using Attendance.API.Context;
using Attendance.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Attendance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAttendanceController : ControllerBase
    {
        private AttendanceContext _db;
        public EmployeeAttendanceController(AttendanceContext db)
        {
            _db = db;
        }
        // GET: api/<EmployeeAttendanceController>
        [HttpGet]
        public IEnumerable<EmployeeAttendance> Get()
        {
            return _db.EmployeeAttendances;
        }

       

        // POST api/<EmployeeAttendanceController>
        [HttpPost]
        public void Post([FromBody] EmployeeAttendance value)
        {
            _db.EmployeeAttendances.Add(value);
            _db.SaveChanges();
        }

        
    }
}
