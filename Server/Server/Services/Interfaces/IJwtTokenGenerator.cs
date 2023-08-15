namespace Server.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}