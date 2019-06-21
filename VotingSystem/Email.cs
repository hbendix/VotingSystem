using System.ComponentModel.DataAnnotations;

namespace VotingSystemEntities
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
