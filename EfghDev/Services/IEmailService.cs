using static EfghDev.Components.Pages.Contact;

namespace EfghDev.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactEmailAsync(ContactFormModel model);
    }
}
