using last_try_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace last_try_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ComplaintController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment; 
        }

        // Get All Complaints
        [HttpGet("GetComplaints/{Id}")]
        public IActionResult GetComplaints(int Id)
        {
            // in the db , Users Table I got 2 admins With (Id 1 And 8) 
            if (Id == 16)
            {
                var complaints = _context.Complaints.ToList();
                return Ok(complaints);
            }
            return Unauthorized( new { message = "You Dont Have Premmesion ." });
            
        }




        // Get All Complaints For single user

        [HttpGet("GetUserComplaints/{id}")]
        public IActionResult GetUserComplaints(int id)
        {
            var userComplaints = _context.Complaints.Where(c => c.UserId == id).ToList();
            return Ok(userComplaints);
        }





        // Get Single Complaint
        [HttpGet("GetSingleComplaint/{id}")]
        public IActionResult GetSingleComplaint(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint == null)
            {
                return NotFound();
            }

            return Ok(complaint);
        }

        // Edit EditComplaint, id Of Complaint.
        [HttpPut("EditComplaint/{id}")]
        public IActionResult EditComplaint(int id, [FromBody] Complaint updatedComplaint)
        {
            var existingComplaint = _context.Complaints.Find(id);

            if (existingComplaint == null)
            {
                return NotFound();
            }

            // Check if the complaint is approved, and if it is, prevent editing
            if (existingComplaint.IsApproved)
            {
                return Unauthorized("Complaint is already approved and cannot be edited.");
            }

            // Update properties of the existing complaint
            existingComplaint.ComplaintText = updatedComplaint.ComplaintText;
            existingComplaint.Language = updatedComplaint.Language;

            _context.SaveChanges(); // Save changes to the database

            return Ok(existingComplaint);
        }


        // Post An complaint Depend in the userId.
        //[HttpPost("sendcomplaint")]
        //public async Task<IActionResult> SendComplaint([FromBody] Complaint complaint)
        //{
        //    // Validate the complaint model
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    // Add the complaint to the context and save changes
        //    _context.Complaints.Add(complaint);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Complaint sent successfully" ,complaint.IsApproved});
        //}
        [HttpPost("sendcomplaint")]
        public async Task<IActionResult> sendcomplaint( Complaint complaint)
                {
                    // Save image to a specific directory within the project
                    _context.Complaints.Add(complaint);
                    await _context.SaveChangesAsync();
                    // Return a success response if necessary
                    return Ok("Image uploaded successfully.");
                }
               

        




    }
}
