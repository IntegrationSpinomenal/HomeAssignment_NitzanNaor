using Models;
using DTOs;

namespace Service
{
    public interface ISessionService
    {
        bool AddPlayer(Player player);
        Player? GetPlayer(long externalId);
		decimal? GetBalance(long externalId);
		//(bool Success, string? Error, int ErrorCode, Player? Player) Transfer(TransferRequestDTO request);
	}
}