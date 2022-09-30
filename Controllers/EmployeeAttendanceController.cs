using Attendance.API.Context;
using Attendance.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        public IEnumerable<EmployeeAttendance> Get(string createDate)
        {
              DateTime cdate = DateTime.ParseExact(createDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
              var filteredData = _db.EmployeeAttendances.ToList().Where(a => a.CreateDateTime.ToShortDateString() == cdate.ToShortDateString());
              return filteredData;
            
        }

       

        // POST api/<EmployeeAttendanceController>
        [HttpPost]
        public void Post([FromBody] EmployeeAttendance value)
        {
            value.CreateDateTime = DateTime.Now;
            _db.EmployeeAttendances.Add(value);
            _db.SaveChanges();
        }

        
    }
}
