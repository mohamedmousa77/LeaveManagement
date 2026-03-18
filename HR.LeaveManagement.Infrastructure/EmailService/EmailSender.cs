using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;

namespace HR.LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(EmailMessage email)
    {
        try
        {
            var options = new RestClientOptions(_emailSettings.ApiUrl);
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", _emailSettings.AuthToken);

            var payload = new
            {
                from = _emailSettings.FromAddress,
                to = email.To,
                subject = email.Subject,
                content = email.Body
            };

            request.AddJsonBody(payload);

            var response = await client.PostAsync(request);

            if (response.IsSuccessful)
            {
                _logger.LogInformation("Email sent successfully to {To}", email.To);
                return true;
            }

            _logger.LogWarning("Email sending failed. Status: {StatusCode}, Content: {Content}",
                response.StatusCode, response.Content);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {To}", email.To);
            return false;
        }
    }
}
