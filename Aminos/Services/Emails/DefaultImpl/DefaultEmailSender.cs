using Aminos.Core.Services.Injections.Attrbutes;

namespace Aminos.Services.Emails.DefaultImpl;

[RegisterInjectable(typeof(IEmailSender))]
public class DefaultEmailSender : IEmailSender
{
    public ValueTask<bool> SendEmail(string sendTo, string subject, string message)
    {
        return ValueTask.FromResult(false);
    }
}