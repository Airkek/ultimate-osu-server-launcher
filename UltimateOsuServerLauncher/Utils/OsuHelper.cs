using System;
using System.IO;
using Microsoft.Win32;

namespace UltimateOsuServerLauncher.Utils
{
    public class OsuHelper
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string ProgramFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        private static readonly string[] CommonOsuPaths =
        {
            Path.Combine(AppData, "osu!"),
            Path.Combine(ProgramFiles, "osu!")
        };

        public static bool IsValidOsuPath(string path)
        {
            var exe = path.EndsWith("osu!.exe");
            
            if (!exe && !Directory.Exists(path))
                return false;
                
            var osu = exe ? path : Path.Combine(path, "osu!.exe");
            
            return File.Exists(osu);
        }
        
        public static string GetOsuFilePath()
        {
            foreach (var path in CommonOsuPaths)
            {
                if(IsValidOsuPath(path))
                    return Path.Combine(path, "osu!.exe");
            }

            var dialog = new OpenFileDialog
            {
                Filter = "osu!|osu!.exe"
            };

            var success = dialog.ShowDialog() ?? false;

            return success ? dialog.FileName : string.Empty;
        } 
    }
}