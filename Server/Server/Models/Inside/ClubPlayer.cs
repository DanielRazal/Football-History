namespace Server.Models.Inside
{
    public class ClubPlayer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Club")]
        public int ClubId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        // [JsonIgnore]
        public virtual Club Club { get; set; } = null!;
        // [JsonIgnore]
        public virtual Player Player { get; set; } = null!;
    }
}