namespace Aminos.Services.Emails
{
	public interface IEmailSender
	{
		ValueTask<bool> SendEmail(string sendTo, string subject, string message);
	}
}
