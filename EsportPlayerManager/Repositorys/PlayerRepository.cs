using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EsportPlayerManager.Models;
using Npgsql;

namespace EsportPlayerManager.Repositorys;

public class PlayerRepository
{
    private readonly string _connectionString;

    public PlayerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitDb()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS players (
            id SERIAL PRIMARY KEY,
            game TEXT NOT NULL,
            skill_level INT NOT NULL,
            stress_level INT NOT NULL,
            fatigue_level INT NOT NULL
            )
        """);
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        var players = await connection.QueryAsync<Player>("SELECT * FROM players");
        return players.ToList();
    }
}