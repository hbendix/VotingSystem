using VotingSystemEntities;

namespace VotingSystemServices.Interfaces
{
    public interface IEmailServices
    {
        Email GenerateRegistrationEmail(string senderEmail, string recipientEmail, User registeredUser);
        Email GenerateElectionReminderEmail(string senderEmail, string recipientEmail, Election election);
        void SendEmail(Email email);
    }
}
