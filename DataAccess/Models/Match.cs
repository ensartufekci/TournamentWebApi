using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; } // primary key
        public int HomeTeamId { get; set; } // foreign key
        public int AwayTeamId { get; set; } // foreign key
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int Week { get; set; }
        [DefaultValue(false)]
        public bool IsPlayed { get; set; }
    }
}