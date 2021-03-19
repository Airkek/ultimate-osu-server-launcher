using System.Diagnostics;
using System.IO.Pipes;
using UltimateOsuServerLauncher.Models;
using UltimateOsuServerLauncher.Utils;

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
        
        private string OsuPath 
        {
            get => Config.OsuPath;
            set => Config.OsuPath = value;
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
            
            Config.Save();
        }
        
        public void PreviousServer()
        {
            if (!IsFirstServer)
                CurrentServerIndex--;
            
            Config.Save();
        }

        public void Launch()
        {
            if (!OsuHelper.IsValidOsuPath(Config.OsuPath))
            {
                OsuPath = OsuHelper.GetOsuFilePath();
                Config.Save();
            }
            
            Process.Start(OsuPath, "-devserver " + CurrentServer.DevServerArgument);
        }
    }
}