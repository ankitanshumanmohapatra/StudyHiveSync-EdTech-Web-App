//create new file( EmailService.cs )                                                                                                     using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var emailSettings = _configuration.GetSection("EmailSettings");

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
        emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
        emailMessage.Subject = subject;                                                                                 //Defind in accountcontroller
        emailMessage.Body = new TextPart("plain") { Text = message };

        using (var client = new SmtpClient())                                                                           //SMTP in appsetting.json
        {
            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
            await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}





//create new file( EmailService.cs )                                                                                                     using MailKit.Net.Smtp;
//using MimeKit;
//using Microsoft.Extensions.Configuration;
//using System.Threading.Tasks;
//using MailKit.Net.Smtp;

//public class EmailService
//{
//    private readonly IConfiguration _configuration;

//    public EmailService(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    public async Task SendEmailAsync(string toEmail, string subject, string message)
//    {
//        var emailSettings = _configuration.GetSection("EmailSettings");

//        var emailMessage = new MimeMessage();
//        emailMessage.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
//        emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
//        emailMessage.Subject = subject;
//        emailMessage.Body = new TextPart("plain") { Text = message };

//        using (var client = new SmtpClient())
//        {
//            await client.ConnectAsync(emailSettings["SmtpServer"], int.Parse(emailSettings["SmtpPort"]), false);
//            await client.AuthenticateAsync(emailSettings["Username"], emailSettings["Password"]);
//            await client.SendAsync(emailMessage);
//            await client.DisconnectAsync(true);
//        }
//    }
//}


//using System.Net;
//using System.Net.Mail;
//using MailKit.Net.Smtp;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;

//namespace StudyHiveSync2.Services
//{
//    public class EmailService : IEmailService
//    {
//        private readonly IConfiguration _configuration;

//        public EmailService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public async Task SendEmailAsync(string toEmail, string subject, string message)
//        {
//            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
//            {
//                Port = int.Parse(_configuration["Smtp:Port"]),
//                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
//                EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"])
//            };

//            var mailMessage = new MailMessage
//            {
//                From = new MailAddress(_configuration["Smtp:FromEmail"]),
//                Subject = subject,
//                Body = message,
//                IsBodyHtml = true,
//            };

//            mailMessage.To.Add(toEmail);

//            await smtpClient.SendMailAsync(mailMessage);
//        }
//    }
//}