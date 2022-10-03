using Attendance.API.UtilityHelpers;
using Attendance.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("UserLogin")]
        [HttpPost]
        public ResponseVM RequestToken([FromBody] LoginVM objVM)
        {

            if(objVM.username == "aAQWEEEESSSTT@0987654321")
            {
                return new ResponseVM { Status = "Success", Message = TokenManager.GenerateToken(objVM.username) };
            }
            return new ResponseVM { Status = "Invalid", Message = "Invalid User." };

        }
        [Route("Validate")]
        [HttpPost]
        public ResponseVM Validate([FromBody] ValidateModel validateModel)
        {
         
          
            if (validateModel.username == "") return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
            if (validateModel.username != "aAQWEEEESSSTT@0987654321") return new ResponseVM { Status = "Invalid", Message = "Invalid User." };
            string tokenUsername = TokenManager.ValidateToken(validateModel.token);
            if (validateModel.username.Equals(tokenUsername))
            {
                return new ResponseVM
                {
                    Status = "Success",
                    Message = "OK",
                };
            }
            return new ResponseVM { Status = "Invalid", Message = "Invalid Token." };
        }
    }
}
