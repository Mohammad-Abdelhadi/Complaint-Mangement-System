using last_try_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace last_try_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComplaintController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Complaints
        [HttpGet("GetComplaints")]
        public IActionResult GetComplaints()
        {
            var complaints = _context.Complaints.ToList();
            return Ok(complaints);
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

        // Edit EditComplaint 
        [HttpPut("EditComplaint/{id}")]
        public IActionResult EditComplaint(int id, [FromBody] Complaint updatedComplaint)
        {
            var existingComplaint = _context.Complaints.Find(id);

            if (existingComplaint == null)
            {
                return NotFound(); // Return 404 Not Found if complaint with given id is not found
            }

 
            // You might want to validate and update other properties as needed
            _context.Complaints.Update(updatedComplaint);

            return Ok(updatedComplaint);
        }

        // Post An complaint Depend in the userId.
        [HttpPost("sendcomplaint")]
        public async Task<IActionResult> SendComplaint([FromBody] Complaint complaint)
        {
            // Validate the complaint model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add the complaint to the context and save changes
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Complaint sent successfully" });
        }




    }
}
