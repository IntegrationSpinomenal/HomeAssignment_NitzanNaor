using Models;
using DTOs;

namespace Service
{
    public class SessionService : ISessionService
    {
        private readonly List<Player> _players = new();

        public bool AddPlayer(Player player)
        {
            // check if player exists
            var newPlayer = _players.FirstOrDefault(p => p.ExternalId == player.ExternalId);
            if (newPlayer == null)
            {
                _players.Add(newPlayer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Player? GetPlayer(long externalId)
        {
            return _players.FirstOrDefault(p => p.ExternalId == externalId);
        }

        public decimal? GetBalance(long externalId)
        {
            var player = GetPlayer(externalId);
            return player?.Balance;
        }

        //public (bool Success, string? Error, int ErrorCode, Player? Player) Transfer(TransferRequestDTO request)
        //{
        //    var player = GetPlayer(request.ExternalId);
        //    if (player == null)
        //        return (false, "No player found.", 404, null);

        //    switch ((string)request.TransactionType)
        //    {
        //        case "DEPOSIT":
        //        case "BONUS":
        //            player.Balance += request.Amount;
        //            break;
        //        case "WITHDRAW":
        //            if (player.Balance < request.Amount)
        //                return (false, "Insufficient funds.", 400, player);
        //            player.Balance -= request.Amount;
        //            break;
        //        case "WITHDRAW_ALL":
        //            player.Balance = 0;
        //            break;
        //        default:
        //            return (false, "Invalid transaction type.", 400, player);
        //    }

        //    return (true, null, 0, player);
        //}
    }
}