using Attendance.API.Context;
using Attendance.API.Models;
using Attendance.API.ViewModels;
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
        public IEnumerable<EntryExitModel> Get(string createDate)
        {
            List<EntryExitModel> attendanceList = new List<EntryExitModel>();
            try
            {
                DateTime cdate = DateTime.ParseExact(createDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //var filteredData = _db.EmployeeAttendances.ToList().Where(a => a.CreateDateTime.ToShortDateString() == cdate.ToShortDateString());

                var resultSum = from row in _db.EmployeeAttendances.AsEnumerable()
                                group row by row.Name into grp
                                select new
                                {
                                    Group1 = grp.Where(y => y.RegistrationType == "لدخول").Where(c => c.CreateDateTime.ToShortDateString() == cdate.ToShortDateString()).FirstOrDefault(),
                                    Group2 = grp.Where(y => y.RegistrationType == "الخروج").Where(c => c.CreateDateTime.ToShortDateString() == cdate.ToShortDateString()).LastOrDefault(),

                                };

                foreach (var rn1 in resultSum)
                {
                    try
                    {
                        EntryExitModel model = new EntryExitModel();
                        model.Name = rn1.Group1.Name != null ? rn1.Group1.Name : rn1.Group2.Name;
                        model.Department = rn1.Group1.Department != null ? rn1.Group1.Department : rn1.Group2.Department;
                        model.EntryTime = rn1.Group1 != null ? rn1.Group1.CreateDateTime : null;
                        model.ExitTime = rn1.Group2 != null ? rn1.Group2.CreateDateTime : null;
                        attendanceList.Add(model);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return attendanceList;
            }
            catch (Exception ex)
            {
                return attendanceList;
            }
            
        }

       

        // POST api/<EmployeeAttendanceController>
        [HttpPost]
        public void Post([FromBody] EmployeeAttendance value)
        {
           // value.CreateDateTime = DateTime.Now.AddHours(10);
            //value.CreateDateTime = DateTime.Now;
            _db.EmployeeAttendances.Add(value);
            _db.SaveChanges();
        }

        
    }
}
