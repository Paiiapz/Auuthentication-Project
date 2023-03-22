using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class forgetpasswordController : ControllerBase
{

    private readonly ILogger<authenticationController> _logger;
    private readonly DatabaseContext _databaseContext;
    public forgetpasswordController(ILogger<authenticationController> logger, DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

   

   

    [HttpPut] // -> https://localhost:5001/user
    public IActionResult Forgetpassword(User user)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Name == user.Name);
            if (_user != null)
            {
                
                _user.Password = user.Password;


                _databaseContext.Users.Update(_user);
                _databaseContext.SaveChanges();
                return Ok(new { message = "Repassword success" });
            }
            else
            {
                return NotFound();

            }
             return Ok(new { message = "Repassword success" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { result = ex.Message, message = "fail" });
        }
    }


}