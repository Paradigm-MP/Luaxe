using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luaxe.Client
{
    public static class Networking
    {
        public static void Initialize()
        {
            Shared.Logging.log.LogMessage($"Networking intiialize");
            Shared.UnityObserver.Awake += Awake;
            Shared.UnityObserver.Start += Start;
            Shared.Logging.log.LogMessage($"Networking intiialized");
        }
        static void Awake()
        {
            Shared.Events.EventSystem.AddListener<Events.LocalPlayerChat>(OnLocalPlayerChat);
        }

        static void Start()
        {
            Shared.Logging.log.LogMessage($"Registering RPC callback for LuaxeNetworkEvent");
            ZRoutedRpc.instance.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
        }

        static bool OnLocalPlayerChat(Events.LocalPlayerChat evt)
        {
            if (evt.text == "/testnet")
            {
                Chat.instance.SendText(Talker.Type.Normal, "Sent test net event to server.");
                Send("testnet");
                return false;
            }

            return true;
        }

        static void Subscribe(string eventName)
        {

        }

        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            Shared.Logging.log.LogInfo($"Got Client RPC_LuaxeNetworkEvent. Package: {package.ToString()}");
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got Client RPC_LuaxeNetworkEvent. Package: {package.ToString()}");
            }
        }

        /// <summary>
        /// Sends an event to the server.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Send(string eventName, params object[] args)
        {
            ZRoutedRpc.instance.InvokeRoutedRPC(0L, "LuaxeNetworkEvent", args);
        }

    }
}
