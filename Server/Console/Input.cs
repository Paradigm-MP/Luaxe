using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Luaxe.Server.Console
{
    public static class Input
    {
        public static void Initialize()
        {
            ThreadStart childref = new ThreadStart(InputThread);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        private static void InputThread()
        {
            while (true)
            {
                string cmd = System.Console.ReadLine();
                if (Shared.Events.EventSystem.Broadcast(new Events.ConsoleCommand(cmd)))
                {
                    // Now fire the command for internal use
                    Shared.Events.EventSystem.Broadcast(new Events.ConsoleCommand(cmd, true));
                }
            }
        }
    }
}
