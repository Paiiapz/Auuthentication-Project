using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;

public class PostInput
{
    public String name { get; set; }
    public String password { get; set; }
}

[ApiController]
[Route("[controller]")]
public class loginController : ControllerBase
{

    private readonly ILogger<authenticationController> _logger;
    private readonly DatabaseContext _databaseContext;
    public loginController(ILogger<authenticationController> logger, DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

   


    [HttpPost] //post login
    public IActionResult login(PostInput input)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Name == input.name
             && o.Password == input.password);

            if (_user == null)
            {
                return Unauthorized();
            }


            if (_user.IsLogin == true)
            {
                return Ok(new { message = "You are already login." });
            }
            _user.IsLogin = true;
            _databaseContext.Users.Update(_user);
            _databaseContext.SaveChanges();

            return Ok(new { message = "Login success" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { result = ex.Message, message = "Fail" });
        }

    }
    

  




}