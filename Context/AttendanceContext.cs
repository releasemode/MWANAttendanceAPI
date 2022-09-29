using Attendance.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.API.Context
{
    public class AttendanceContext
       :DbContext
    {
        public AttendanceContext(DbContextOptions options)
           : base(options)
        {

        }
        public DbSet<EmployeeAttendance> EmployeeAttendances { get; set; }
    }
}
