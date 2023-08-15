namespace Server.Models.Inside
{
    public class Club
    {
        public Club()
        {
            ClubPlayers = new HashSet<ClubPlayer>();
        }

        public int Id { get; set; }
        public ClubSelection ClubName { get; set; } = ClubSelection.None;
        public virtual ICollection<ClubPlayer> ClubPlayers { get; set; }
    }
}
