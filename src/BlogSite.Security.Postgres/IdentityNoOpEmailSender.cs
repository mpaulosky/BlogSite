using Microsoft.AspNetCore.Identity.UI.Services;

namespace BlogSite.Security.Postgres;

// Remove the "else if (EmailSender is IdentityNoOpEmailSender)" block from RegisterConfirmation.razor after updating with a real implementation.
internal sealed class IdentityNoOpEmailSender : IEmailSender<PgBlogSiteUser>
{
	private readonly IEmailSender emailSender = new NoOpEmailSender();

	public Task SendConfirmationLinkAsync(PgBlogSiteUser user, string email, string confirmationLink) =>
			emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

	public Task SendPasswordResetLinkAsync(PgBlogSiteUser user, string email, string resetLink) =>
			emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

	public Task SendPasswordResetCodeAsync(PgBlogSiteUser user, string email, string resetCode) =>
			emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
}