using System;
using VotingSystemEntities;
using VotingSystemServices.Interfaces;

namespace VotingSystemServices
{
    public class EmailServices : IEmailServices
    {
        public Email GenerateRegistrationEmail(string senderEmail, string recipientEmail, User registeredUser)
        {
            string content = $"<p>Congratulations {registeredUser.FullName}, you have registered to vote.</p>" +
                             "<p>When you vote you can log in using the following details:</p>" +
                             $"<table><tr><td>Username</td><td>{registeredUser.Username}<td></tr><tr><td>Username</td><td>{registeredUser.PasswordHash}<td></tr></table>";

            Email registrationEmail = new Email
            {
                SenderEmail = senderEmail,
                RecipientEmail = recipientEmail,
                Subject = "You are now registered",
                Content = content
            };

            return registrationEmail;
        }

        public Email GenerateElectionReminderEmail(string senderEmail, string recipientEmail, Election election)
        {
            string content = "<p>Hello</p>" +
                             $"<p>Voting is now open for {election.ElectionName}. You have until {election.EndDate.ToString("F")} to cast your vote.</p>";

            Email reminderEmail = new Email
            {
                SenderEmail = senderEmail,
                RecipientEmail = recipientEmail,
                Subject = "Voting is now open",
                Content = content
            }; 

            return reminderEmail;
        }

        public void SendEmail(Email email)
        {
            // todo: Add method from library that sends emails
            throw new NotImplementedException();
        }
    }
}
