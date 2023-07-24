using CRUDAPI.Dal;
using CRUDAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDB db;
        public UserController(MyDB db)
        {
            this.db = db;
        }
    
        private IEnumerable<User> GetAccounts()
        {
           
            var res = from c in db.users
                      select c;

            return res;
        }
    
        
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var user = GetAccounts().ToList();
                if (user.Count == 0)
                {
                    return NotFound("not available");
                }
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = GetAccounts().FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return NotFound($"not found with ud {id}");
                }
                return Ok(user);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPost]
        public IActionResult Post(User user)
        {
            try
            {
                db.Add(user);
                db.SaveChanges();
                return Ok("user created");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(User user)
        {
            if(user == null || user.Id == 0)
            {
                if (user == null)
                {
                    return BadRequest("data is invalid");

                }
                else
                {
                    return BadRequest($"user not found with id{user.Id}");
                }
            }
            var us = GetAccounts().FirstOrDefault(id => id.Id == user.Id); 
            if(us == null)
                    return NotFound($"user not found with id{user.Id}");
            us.Username
                = user.Username;
            us.Password = user.Password;
            us.Email = user.Email;
            us.isActive = user.isActive;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("details uptadet");

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = GetAccounts().FirstOrDefault(user=> user.Id == id);
            if (user == null)
            {
                return NotFound("user not found");
            }
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();
            return Ok("deleted");
            
        }


    }
}
