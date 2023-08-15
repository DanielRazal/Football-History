namespace Server.Repositories.Inside_Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayers();
        Task<Player> GetPlayerById(int id);
        Task<ComparePlayersResultDTO> ComparePlayers(int firstId, int secondId);
    }
}
