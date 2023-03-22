using Microsoft.AspNetCore.Mvc;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebAPI.Controllers;



[ApiController]
[Route("[controller]")]
public class authenticationController : ControllerBase
{

    private readonly ILogger<authenticationController> _logger;
    private readonly DatabaseContext _databaseContext;
    public authenticationController(ILogger<authenticationController> logger, DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        try
        {
            var users = _databaseContext.Users.ToList();
            return Ok(new { result = users, message = "success" });

        }
        catch (Exception ex)
        {
            return StatusCode(500, new { result = ex.Message, message = "Fail" });
        }

    }


   

  
    [HttpPost]
    public IActionResult CreateUsers(User user)
    {
        try
        {
            _databaseContext.Users.Add(user); //create Add command
            _databaseContext.SaveChanges(); //commit new user to database
            return Ok(new {message = "Add user success"});
        } 
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "Add user fail"});
        }
    }
    [HttpDelete("{Id}")]
    public IActionResult DeleteUsers(int id)
    {
        try
        {
            var _user = _databaseContext.Users.SingleOrDefault(o => o.Id == id);
            if(_user != null)
            {
                _databaseContext.Remove(_user);
                _databaseContext.SaveChanges();
                return Ok(new {message = "delete user success"});
            }
            else
            {
                return Ok(new {message = "delete user fail"});
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {result = ex.Message, message = "fail"});
        }
    }





}