using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; } // primary key
        [MinLength(2)]
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GoalFor { get; set; }
        public int GoalAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
    }
}