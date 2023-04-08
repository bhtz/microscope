using System.Net.Mail;

namespace Microscope.ExternalSystems.Services;

public interface IMailService
{   
    Task<bool> SendOnGridClosedMail(string To, object data);
    Task<bool> SendTemplateOwnerAddedMail(string To, object data);
    Task<bool> SendUserInvitation(List<string> emails);
    Task<bool> SendGridCreatedMail(string To, object data);
}