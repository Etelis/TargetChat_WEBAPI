#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using targetchatserver.Data;
using targetchatserver.Models;

namespace targetchatserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelsController : ControllerBase
    {
        private readonly targetchatserverContext _context;

        public UserModelsController(targetchatserverContext context)
        {
            _context = context;
        }

        // GET: api/UserModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserModel()
        {
            return await _context.UserModel.ToListAsync();
        }

        // GET: api/UserModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(string id)
        {
            var userModel = await _context.UserModel.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return userModel;
        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel(string id, UserModel userModel)
        {
            if (id != userModel.Username)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            _context.UserModel.Add(userModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserModelExists(userModel.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserModel", new { id = userModel.Username }, userModel);
        }

        // DELETE: api/UserModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel(string id)
        {
            var userModel = await _context.UserModel.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.UserModel.Remove(userModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserModelExists(string id)
        {
            return _context.UserModel.Any(e => e.Username == id);
        }
    }
}
