using System.IO.Pipes;
using UltimateOsuServerLauncher.Models;

namespace UltimateOsuServerLauncher
{
    public class ServerLauncher
    {
        private Config Config = Config.Read();
        private Server[] Servers => Config.Servers;

        private int CurrentServerIndex 
        {
            get => Config.ServerIndex;
            set => Config.ServerIndex = value;
        }

        public Server CurrentServer => Servers[CurrentServerIndex];
        
        public bool IsFirstServer => CurrentServerIndex == 0;
        public bool IsLastServer => CurrentServerIndex == Servers.Length - 1;
        
        public ServerLauncher()
        {
            if (Servers == null || Servers.Length == 0)
            {
                Config.SetDefault();
                Config.Save();
            }
            else if (CurrentServerIndex >= Servers.Length || CurrentServerIndex < 0)
            {
                CurrentServerIndex = 0;
                Config.Save();
            }
        }

        public void NextServer()
        {
            if (!IsLastServer)
                CurrentServerIndex++;
        }
        
        public void PreviousServer()
        {
            if (!IsFirstServer)
                CurrentServerIndex--;
        }
    }
}