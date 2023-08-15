
using Newtonsoft.Json.Serialization;

namespace Server.Models.Inside
{
    public class Player
    {

        [Key]
        public int Id { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int GoalContributions { get; set; }
        public int Titles { get; set; }
        public int Appearances { get; set; }
        public string DateOfBirth { get; set; } = string.Empty;
        public double GoalsRatio { get; set; }
        public double AssistsRatio { get; set; }
        public string Information { get; set; } = string.Empty;
        public int Score { get; set; }
        public int PositionId { get; set; }
        public int NationalityId { get; set; }

        public virtual Position Position { get; set; } = null!;
        public virtual Nationality Nationality { get; set; } = null!;
        public virtual ICollection<ClubPlayer> ClubPlayers { get; set; } = null!;

        // image , ballondor,golden boot , chanpions league goals and assists,nantional goals and assists,word cup,
        // euro , copa america  
    }
}
