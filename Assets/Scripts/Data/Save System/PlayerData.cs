namespace SurviveTogether.Data
{
    [System.Serializable]
    public class PlayerData
    {
        public string id;

        public  static PlayerData GenerateInitialPlayerData(string id)
        {
            PlayerData player = new PlayerData();
            player.id = id;
            return player;
        }
    }
}