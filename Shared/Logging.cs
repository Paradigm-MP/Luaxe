using BepInEx;
using BepInEx.Logging;

namespace Luaxe.Shared
{
    public static class Logging
    {
        public static ManualLogSource log;

        public static void Initialize()
        {
            log = BepInEx.Logging.Logger.CreateLogSource("Luaxe");
        }
    }
}