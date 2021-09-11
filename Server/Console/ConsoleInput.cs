using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Luaxe.Server.Console
{
    public static class Input
    {
        private static bool shouldContinue = true;
        public static void Initialize()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += (sender, e) =>
            {
                InputThread();
            };
            worker.RunWorkerCompleted += (sender, e) =>
            {
                // When the input thread finishes, that means the stop command has been sent
                if (shouldContinue)
                {
                    Shared.Logging.log.LogError("Input worker completed without ServerStopCommand being issued.\n" + 
                                                "Something has probably gone terribly wrong and you should check recent changes.\n\n" + 
                                                "Restarting Input worker thread...");
                    worker.RunWorkerAsync();
                    return;
                }

                Shared.Logging.log.LogInfo("Stopping server...");
                Environment.Exit(0);
            };
            worker.RunWorkerAsync();

            Shared.Events.EventSystem.AddListener(delegate (Events.ServerStopCommand evt)
            {
                shouldContinue = false;
                return true;
            });
        }

        private static void InputThread()
        {
            Shared.Logging.log.LogInfo("Console Input worker thread started.");
            while (shouldContinue)
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
