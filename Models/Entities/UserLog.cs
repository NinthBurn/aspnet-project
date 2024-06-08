using System.ComponentModel.DataAnnotations.Schema;

namespace Lab10.Models.Entities
{
    public class UserLog
    {
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime LogDate { get; set; }
        public string LogType { get; set; }
        public string LogSeverity { get; set; }
        public string LogContent { get; set; }

    }
}
