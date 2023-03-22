using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class logoutController : ControllerBase
{

    private readonly ILogger<authenticationController> _logger;
    private readonly DatabaseContext _databaseContext;
    public logoutController(ILogger<authenticationController> logger, DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    
    [HttpPost] //post logout
    public IActionResult logout(User user)
    {
        try

        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Name == user.Name);

            if (_user == null)
            {
                return NotFound(404);
            }

            if (_user.IsLogin == false)
            {
                return Ok(new { message = "You are already logout." });
            }
            _user.IsLogin = false;
            _databaseContext.Users.Update(_user);
            _databaseContext.SaveChanges();
            return Ok(new { message = "You are already logout." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { result = ex.Message, message = "Fail" });
        }

    }


    




}