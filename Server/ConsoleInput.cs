using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace Luaxe.Server
{
    public static class ConsoleInput
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
                    // TODO: call the internal command api if the command matches, such as "reload"
                }
            }
        }
    }
}
