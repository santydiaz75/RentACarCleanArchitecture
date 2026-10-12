using RentACarCleanArchitecture.Application.Abstractions.Email;
using RentACarCleanArchitecture.Domain.Users;

namespace RentACarCleanArchitecture.Infrastructure;

internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}