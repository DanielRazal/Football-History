namespace Server.Models.Inside
{
    public class Position
    {
        public int Id { get; set; }
        public PositionSelection PositionName { get; set; } = PositionSelection.None;
    }
}
