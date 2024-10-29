using System.Collections.Generic;
using OOP_Lab.Entities;

namespace OOP_Lab.Service
{
    public static class DataService
    {
        public static List<ApplicationEntitie> ApplicationModels { get; set; } =
            new List<ApplicationEntitie>
            {
                new ApplicationEntitie(
                    "WhatsApp",
                    "WhatsApp from Meta is a completely free messaging app used by over two billion people in over 180 countries.",
                    Enums.ApplicationCategory.Social,
                    4,
                    1
                ),
                new ApplicationEntitie(
                    "Spotify",
                    "Love music and podcasts? Listen to your favorite episodes, tracks, and albums on Spotify for Windows. Play your favorite tracks, explore charts, or choose editorial playlists in any genre and for any mood.",
                    Enums.ApplicationCategory.Music,
                    3,
                    10
                ),
                new ApplicationEntitie(
                    "Notepad",
                    "Notepad is a free and open source text editor for Windows with syntax highlighting, markup, and VHDL and Verilog hardware description languages.",
                    Enums.ApplicationCategory.Productivity,
                    5,
                    3
                ),
            };

        public static int ApplicationsLimit { get; set; } = -1;

        public static bool isLimitReached()
        {
            return ApplicationsLimit != -1 && ApplicationModels.Count >= ApplicationsLimit;
        }
    }
}
