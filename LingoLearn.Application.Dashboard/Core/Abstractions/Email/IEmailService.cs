using LingoLearn.Contracts.Email;

namespace LingoLearn.Application.Dashboard.Core.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailRequest sendEmailRequest);
}