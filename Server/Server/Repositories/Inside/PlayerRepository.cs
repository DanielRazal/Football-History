namespace Server.Repositories.Inside
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ServerContext _context;
        public PlayerRepository(ServerContext context)
        {
            _context = context;
        }

        public async Task<ComparePlayersResultDTO> ComparePlayers(int firstId, int secondId)
        {
            var player1 = await _context.Players.FirstOrDefaultAsync(u => u.Id == firstId);
            var player2 = await _context.Players.FirstOrDefaultAsync(u => u.Id == secondId);

            if (player1 != null && player2 != null)
            {
                var score1 = CalculatePlayerScore(player1);
                var score2 = CalculatePlayerScore(player2);

                string score1Color = (score1 > score2) ? "green" : "red";
                string score2Color = (score2 > score1) ? "green" : "red";

                if (score1 == score2)
                {
                    return new ComparePlayersResultDTO
                    {
                        Message = $"{player1.FullName} and {player2.FullName} have the same score {score1}"
                    };
                }
                else if (score1 > score2)
                {
                    return new ComparePlayersResultDTO
                    {
                        Message = $"<span style='font-weight: bold'>{player1.FullName}</span> has a score of <span style='color: {score1Color}'>{score1}</span>, which is higher than <span style='font-weight: bold'>{player2.FullName}</span>'s score of <span style='color: {score2Color}'>{score2}</span>."
                    };
                }
                else
                {
                    return new ComparePlayersResultDTO
                    {
                        Message = $"<span style='font-weight: bold'>{player2.FullName}</span> has a score of <span style='color: {score2Color}'>{score2}</span>, which is higher than <span style='font-weight: bold'>{player1.FullName}</span>'s score of <span style='color: {score1Color}'>{score1}</span>."
                    };
                }
            }

            else return new ComparePlayersResultDTO
            {
                Message = "Players not found"
            };
        }

        private int CalculatePlayerScore(Player player)
        {
            int score = (int)(player.Goals * 2 + player.Assists * 1 + player.Appearances * 0.5
                + player.Titles * 10 + player.GoalContributions * 2.5 +
                player.GoalsRatio * 150 + player.AssistsRatio * 100);
            return score;
        }


        public async Task<List<Player>> GetAllPlayers()
        {
            return await _context.Players.Include(c => c.Position).Include(c => c.Nationality).
            Include(c => c.ClubPlayers)
                .ThenInclude(cp => cp.Club)
                .ToListAsync();
        }

        public async Task<Player> GetPlayerById(int id)
        {
            var player = await _context.Players.Include(c => c.Position).Include(c => c.Nationality).
            Include(c => c.ClubPlayers).ThenInclude(cp => cp.Club).FirstOrDefaultAsync(u => u.Id == id);
            if (player != null)
                return player;
            else return null!;
        }
    }

}
