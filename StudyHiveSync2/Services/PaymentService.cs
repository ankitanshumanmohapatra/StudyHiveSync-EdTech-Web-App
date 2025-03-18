using Microsoft.EntityFrameworkCore;
using StudyHiveSync2.Data;
using StudyHiveSync2.Model;

namespace StudyHiveSync2.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public PaymentService(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<Payment> MakePaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.Include(p => p.User).Include(p => p.Course).ToListAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.Include(p => p.User).Include(p => p.Course).FirstOrDefaultAsync(p => p.PaymentId == paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(int userId)
        {
            return await _context.Payments.Include(p => p.User).Include(p => p.Course).Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await _emailService.SendEmailAsync(toEmail, subject, message);
        }
    }
}
