using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Luaxe.Server.Console
{
    public static class Commands
    {
        public static void Initialize()
        {
            Shared.Events.EventSystem.AddListener<Events.ConsoleCommand>(OnConsoleCommand);
        }

        static bool OnConsoleCommand(Events.ConsoleCommand evt)
        {
            if (!evt.isInternal) { return true; }

            if (evt.command == "reload")
            {
                ReloadCommand();
            }
            else if (evt.command == "clear")
            {
                ClearCommand();
            }
            else if (evt.command == "stop")
            {
                Shared.Logging.log.LogInfo("Stop command received, please wait...");
                Shared.Events.EventSystem.Broadcast(new Events.ServerStopCommand());
            }

            return true;
        }

        public static void ReloadCommand()
        {
            // TODO: reload lua scripts
        }

        public static void ClearCommand()
        {
            System.Console.Clear();
        }
    }
}
