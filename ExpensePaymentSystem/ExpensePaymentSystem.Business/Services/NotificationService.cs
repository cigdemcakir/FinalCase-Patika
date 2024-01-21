using System.Net;
using System.Net.Mail;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Serilog;

namespace ExpensePaymentSystem.Business.Services;
public class NotificationService : INotificationService
{
    private readonly ExpensePaymentSystemDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress; 

    public NotificationService(ExpensePaymentSystemDbContext context, IConfiguration configuration, SmtpClient smtpClient)
    {
        _context = context;
        _configuration = configuration;
        _smtpClient = smtpClient;
        _fromAddress = _configuration["EmailSettings:FromAddress"];
    }

    public async Task SendExpensePaymentNotificationAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            await SendExpensePaymentEmailAsync(user.Email, "Expense Notification", "Your expense have been paid.");
        }
    }

    public async Task SendExpensePaymentEmailAsync(string to, string subject, string body)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await _smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Email could not be sent.");
        }
    }
}