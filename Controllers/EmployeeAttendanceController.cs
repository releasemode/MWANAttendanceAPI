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
                
                var list = _db.EmployeeAttendances.Where(a=>a.CreateDateTime >= cdate).Where(a => a.CreateDateTime <= cdate.AddDays(1)).AsEnumerable().GroupBy(x => new { x.Name, x.CreateDateTime.Date })
                     .Select(x => new
                     {
                         Name = x.Key.Name,

                         Group1 = x.Where(y => y.RegistrationType == "لدخول").FirstOrDefault(),
                         Group2 = x.Where(y => y.RegistrationType == "الخروج").LastOrDefault(),
                         Group3 = x.Where(y => y.RegistrationType == "إستئذان").LastOrDefault(),

                     })
                    .Select(x => new EntryExitModel
                    {
                        Name = x.Name,
                        Department = x.Group1 == null ? x.Group2.Department : x.Group1.Department,
                        EntryTime = x.Group1 == null ? null : x.Group1.CreateDateTime,
                        ExitTime = x.Group2 == null ? null : x.Group2.CreateDateTime,
                        EarlyExitTime = x.Group3 == null ? null : x.Group3.CreateDateTime,
                    }).ToList();

                foreach (var attendanceEntry in list)
                {
                    EntryExitModel objList = new EntryExitModel();
                    objList.Name = attendanceEntry.Name;
                    objList.Department = attendanceEntry.Department;
                    objList.EntryTime = attendanceEntry.EntryTime;
                    objList.ExitTime = attendanceEntry.ExitTime;
                    objList.EarlyExitTime = attendanceEntry.EarlyExitTime;
                    attendanceList.Add(objList);
                }
               
                return attendanceList;


            }
            catch (Exception ex)
            {
                return attendanceList;
            }
            
        }

       
        [HttpGet]
        [Route("AttendanceStatus")]
        public IEnumerable<EntryExitModel> AttendanceStatus(string name)
        {
            List<EntryExitModel> attendanceStatusList = new List<EntryExitModel>();
            try
            {
                //DateTime cdate = DateTime.ParseExact(createDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var list = _db.EmployeeAttendances
                              .Where(a => a.CreateDateTime >= DateTime.Today)
                              .Where(a => a.CreateDateTime <= DateTime.Today.AddDays(1))
                              .Where(a=>a.Name == name)
                              .AsEnumerable().GroupBy(x => new { x.Name, x.CreateDateTime.Date })
                     .Select(x => new
                     {
                         Name = x.Key.Name,

                         Group1 = x.Where(y => y.RegistrationType == "لدخول").FirstOrDefault(),
                         Group2 = x.Where(y => y.RegistrationType == "الخروج").LastOrDefault(),
                         Group3 = x.Where(y => y.RegistrationType == "إستئذان").LastOrDefault(),

                     })
                    .Select(x => new EntryExitModel
                    {
                        Name = x.Name,
                        EntryTime = x.Group1 == null ? null : x.Group1.CreateDateTime,
                        ExitTime = x.Group2 == null ? null : x.Group2.CreateDateTime,
                        EarlyExitTime = x.Group3 == null ? null : x.Group3.CreateDateTime,
                    }).ToList();

                foreach (var attendanceEntry in list)
                {
                    EntryExitModel objList = new EntryExitModel();
                    objList.Name = attendanceEntry.Name;
                    objList.EntryTime = attendanceEntry.EntryTime;
                    objList.ExitTime = attendanceEntry.ExitTime;
                    objList.EarlyExitTime = attendanceEntry.EarlyExitTime;
                    attendanceStatusList.Add(objList);
                }

                return attendanceStatusList;


            }
            catch (Exception ex)
            {
                return attendanceStatusList;
            }
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
