using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyHiveSync2.Data;
using StudyHiveSync2.Model;
using StudyHiveSync2.Services;

namespace StudyHiveSync2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly ApplicationDbContext _context;

        public PaymentController(PaymentService paymentService, ApplicationDbContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
        [Authorize(Roles ="User")]
        [HttpPost("make")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto paymentDto)
        {
            var user = await _context.Users.FindAsync(paymentDto.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var course = await _context.Courses.FindAsync(paymentDto.CourseId);
            if (course == null)
            {
                return NotFound("Course not found");
            }

            var payment = new Payment
            {
                UserId = paymentDto.UserId,
                User = user,
                CourseId = paymentDto.CourseId,
                Course = course,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentDate = DateTime.Now
            };

            var result = await _paymentService.MakePaymentAsync(payment);
            if (result == null)
            {
                return BadRequest("Payment processing failed");
            }

            var subject = "Payment Confirmation";
            var body = $"Your payment of {paymentDto.Amount} for course ID {paymentDto.CourseId} has been successfully processed.";
            await _paymentService.SendEmailAsync(user.Email, subject, body);

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPaymentsByUserId(int userId)
        {
            var payments = await _paymentService.GetPaymentsByUserIdAsync(userId);
            if (payments == null || !payments.Any())
            {
                return NotFound("No payments found for the specified user");
            }
            return Ok(payments);
        }
    }
}