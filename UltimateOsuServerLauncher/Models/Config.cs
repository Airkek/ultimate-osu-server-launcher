using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using UltimateOsuServerLauncher.Utils;

namespace UltimateOsuServerLauncher.Models
{
    public class Config
    {
        private const string FileName = "LauncherConfig.json";
        
        [JsonProperty("config_version")]
        public string Version;

        [JsonProperty("osu_path")] 
        public string OsuPath;

        [JsonProperty("current_server")] 
        public int ServerIndex;

        [JsonProperty("servers")]
        public Server[] Servers;
        
        public void SetDefault()
        {
            Version = "0.1";
            ServerIndex = 0;
            OsuPath = string.Empty;
            Servers = new[]
            {
                new Server("Bancho", "osu.ppy.sh", "https://osu.ppy.sh", "ppy.sh"),
                new Server("Gatari", "osu.gatari.pw", "https://osu.gatari.pw", "gatari.pw"),
                new Server("Kurikku", "kurikku.pw", "https://kurikku.pw", "kurikku.pw"),
                new Server("Akatsuki", "akatsuki.pw", "https://akatsuki.pw", "akatsuki.pw"),
                //new Server("EZPPFarm", "ez-pp.farm", "https://ez-pp.farm", "ez-pp.farm"),
                //new Server("Ripple", "ripple.moe", "https://ripple.moe", "ripple.moe"),
                new Server("RealistikOsu", "ussr.pl", "https://ussr.pl", "ussr.pl"),
                new Server("Astellia", "astellia.club", "https://astellia.club", "astellia.club"),
                new Server("Shizofrenia", "osu.shizofrenia.pw", "https://osu.shizofrenia.pw", "shizofrenia.pw"),
                new Server("Debian", "debian.moe", "https://debian.moe", "debian.moe"),
                new Server("Horizon", "lemres.de", "https://elemres.de", "lemres.de"),
                new Server("Iteki", "iteki.pw", "https://iteki.pw", "iteki.pw"),
                new Server("Sakuru", "sakuru.pw", "https://sakuru.pw", "sakuru.pw")
            };
        }

        public static Config FromRemote()
        {
            var wc = new WebClient();
            var str = wc.DownloadString("https://raw.githubusercontent.com/Airkek/ultimate-osu-server-launcher/main/default.json");

            return JsonConvert.DeserializeObject<Config>(str);
        }

        public void Save() => File.WriteAllText(FileName, JsonConvert.SerializeObject(this, Formatting.Indented));

        public static Config Read()
        {
            Config cfg;
            
            if (!File.Exists(FileName))
            {
                try
                {
                    cfg = FromRemote();
                }
                catch
                {
                    cfg = new Config();
                    cfg.SetDefault();
                }
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