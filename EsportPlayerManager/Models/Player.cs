namespace EsportPlayerManager.Models;

public class Player
{
    public int ID { get; set; }
    public string NickName { get; set; }
    public string Game { get; set; }
    public int SkillLevel { get; set; }
    public int StressLevel { get; set; }
    public int FatigueLevel { get; set; }
}