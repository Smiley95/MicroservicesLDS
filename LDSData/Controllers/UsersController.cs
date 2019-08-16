using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LDSData.DBContext;
using LDSData.Repositories;

namespace LDSData.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private IGenericRepository<User> repository = null;
       
        public UsersController()
        {
            this.repository = new GenericRepository<User>();
        }

        public UsersController(GenericRepository<User> repository)
        {
            this.repository = repository;
        }
        // GET: api/Users
        public IEnumerable<User> GetUser()
        {
            return repository.GetAll();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // POST: api/Users/
        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetUserByName([FromUri]string username, [FromBody]string password)
        {
            User users = repository.GetAll().FirstOrDefault(c => (c.User_name.Equals(username) && c.User_pwd.Equals(password)))/*.Select(e => e)*/;
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users.User_email);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_email)
            {
                return BadRequest();
            }

            repository.Update(user);

            try
            {
                repository.Save();
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
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(user);

            try
            {
                repository.Save();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.User_email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.User_email }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            repository.Delete(user);
            repository.Save();

            return Ok(user);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        private bool UserExists(string id)
        {
            return repository.GetById(id) != null ? true : false;
        }
    }
}