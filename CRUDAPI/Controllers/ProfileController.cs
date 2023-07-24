using CRUDAPI.Dal;
using CRUDAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ProfileController : Controller
    {

        private readonly MyDB db;
        public ProfileController(MyDB db)
        {
            this.db = db;
        }

     
        private IEnumerable<UserProfile> GetProfiles()
        {

            var res = from c in db.userProfiles
                      select c;

            return res;
        }
        [HttpGet]
        public ActionResult getprofile()
        {
            try
            {
                var user = GetProfiles().ToList();
                if (user.Count == 0)
                {
                    return NotFound("not available");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = GetProfiles().FirstOrDefault(x => x.UserProfileId == id);
                if (user == null)
                {
                    return NotFound($"not found with ud {id}");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post(UserProfile user)
        {
            try
            {
                db.Add(user);
                db.SaveChanges();
                return Ok("user created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(UserProfile user)
        {
            if (user == null || user.UserProfileId == 0)
            {
                if (user == null)
                {
                    return BadRequest("data is invalid");

                }
                else
                {
                    return BadRequest($"user not found with id{user.UserProfileId}");
                }
            }
            var us = GetProfiles().FirstOrDefault(id => id.UserProfileId == user.UserProfileId);
            if (us == null)
                return NotFound($"user not found with id{user.UserProfileId}");
            us.number
                = user.number;
            us.FirstName = user.FirstName;
            us.LastName = user.LastName;
            db.Entry(us).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("details uptadet");

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = GetProfiles().FirstOrDefault(user => user.UserProfileId == id);
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
