namespace Server.Models.Inside
{
    public class Nationality
    {
        public int Id { get; set; }
        public NationalitySelection NationalityName { get; set; } = NationalitySelection.None;
    }
}
