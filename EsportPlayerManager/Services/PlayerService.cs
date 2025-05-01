using EsportPlayerManager.Repositorys;

namespace EsportPlayerManager.Services;

public class PlayerService
{
    private readonly PlayerRepository _playerRepository;

    public PlayerService(PlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
}