using System.IO;
using Newtonsoft.Json;

namespace UltimateOsuServerLauncher.Models
{
    public class Config
    {
        private const string FileName = "LauncherConfig.json";
        
        [JsonProperty("config_version")]
        public string Version;

        [JsonProperty("current_server")] 
        public int ServerIndex;

        [JsonProperty("servers")]
        public Server[] Servers;
        
        public void SetDefault()
        {
            Version = "0.1";
            ServerIndex = 0;
            Servers = new[]
            {
                new Server("Gatari", "osu.gatari.pw", "https://osu.gatari.pw", "gatari.pw"),
                new Server("Kurikku", "kurikku.pw", "https://kurikku.pw", "kurikku.pw"),
                new Server("Akatsuki", "akatsuki.pw", "https://akatsuki.pw", "akatsuki.pw"),
                new Server("Shizofrenia", "osu.shizofrenia.pw", "https://osu.shizofrenia.pw", "shizofrenia.pw"),
            };
        }

        public void Save() => File.WriteAllText(FileName, JsonConvert.SerializeObject(this, Formatting.Indented));

        public static Config Read()
        {
            Config cfg;
            
            if (!File.Exists(FileName))
            {
                cfg = new Config();
                cfg.SetDefault();
                cfg.Save();
            }
            else
            {
                var file = File.ReadAllText(FileName);
                cfg = JsonConvert.DeserializeObject<Config>(file);
            }

            return cfg;
        }
    }
}