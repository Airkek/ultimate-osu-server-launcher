using Newtonsoft.Json;

namespace UltimateOsuServerLauncher.Models
{
    public class Server
    {
        [JsonProperty("display_name")] 
        public string ServerName;
        
        [JsonProperty("display_url")] 
        public string DisplayUrl;

        [JsonProperty("frontend_url")] 
        public string FrontendUrl;

        [JsonProperty("devserver_arg")]
        public string DevServerArgument;

        public Server(string name, string dUrl, string fUrl, string arg)
        {
            ServerName = name;
            DisplayUrl = dUrl;
            FrontendUrl = fUrl;
            DevServerArgument = arg;
        }
    }
}