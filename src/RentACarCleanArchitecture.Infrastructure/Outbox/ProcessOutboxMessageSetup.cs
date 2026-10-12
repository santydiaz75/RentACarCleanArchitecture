using Microsoft.Extensions.Options;
using Quartz;

namespace RentACarCleanArchitecture.Infrastructure.Outbox;

public class ProcessOutboxMessageSetup : IConfigureOptions<QuartzOptions>
{
    public ProcessOutboxMessageSetup(IOptions<OutboxOptions> outboxOptions)
    {
    }

    public void Configure(QuartzOptions options)
    {
    }
}