using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EONReality.Models;
namespace EONReality.Controllers
{
    public class UsersController : ApiController
    {
        private EONRealityContext db = new EONRealityContext();

        // GET: api/Users
        public IQueryable<UserDTO> GetUsers()
        {
            var users = from u in db.Users
                        select new UserDTO()
                        {
                            UserId = u.UserId,
                            Name = u.Name,
                            Email = u.Email,
                            Gender = u.Gender,
                            Dates = u.Dates,
                            ARequest = u.ARequest,
                            DRegister = u.DRegister
                        };
            return users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            var user = await db.Users.Select(u =>
                new UserDTO()
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Gender = u.Gender,
                    Dates = u.Dates,
                    ARequest = u.ARequest,
                    DRegister = u.DRegister
                }).SingleOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(user.DRegister.CompareTo(new DateTime(2019, 1, 1)) < 0)
            {
                ModelState.AddModelError("DRegister", "Date is not allowed");
                return BadRequest(ModelState);
            }
            else if(user.DRegister.CompareTo(new DateTime(2019, 6, 30)) > 0)
            {
                ModelState.AddModelError("DRegister", "Date is not allowed");
                return BadRequest(ModelState);
            }


            db.Users.Add(user);
            await db.SaveChangesAsync();

            var dto = new UserDTO()
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                Dates = user.Dates,
                ARequest = user.ARequest,
                DRegister = user.DRegister
            };

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, dto);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}