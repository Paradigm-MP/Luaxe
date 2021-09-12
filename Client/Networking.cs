using HarmonyLib;
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
            Shared.Logging.log.LogInfo($"Networking intiialize");
            Shared.UnityObserver.Awake += Awake;
            Shared.UnityObserver.Start += Start;
            Shared.Logging.log.LogInfo($"Networking intiialized");
        }
        static void Awake()
        {
            Shared.Events.EventSystem.AddListener<Events.LocalPlayerChat>(OnLocalPlayerChat);
        }

        static void Start()
        {
            Shared.Logging.log.LogInfo($"Registering RPC callback for LuaxeNetworkEvent");
            ZRoutedRpc.instance.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
            Shared.Logging.log.LogInfo($"Registered RPC callback for LuaxeNetworkEvent");
        }

        static long GetServerPeerID()
        {
            return Traverse.Create(ZRoutedRpc.instance).Method("GetServerPeerID").GetValue<long>();
        }

        static bool OnLocalPlayerChat(Events.LocalPlayerChat evt)
        {
            if (evt.text == "/testnet")
            {
                Shared.Logging.log.LogMessage("Sending to server...");
                Send("testnet");
                Shared.Logging.log.LogMessage("Sent to server!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when a network event is received from the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="package"></param>
        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got Client RPC_LuaxeNetworkEvent.");
                Shared.Networking.NetworkEventData ned = Shared.Networking.DeserializePackageToNetworkEventData(package);
                Shared.Events.EventSystem.Broadcast(new Events.NetworkEvent(ned));

                ned.LogMetadata();
                ned.LogArgs();
            }
        }

        static void Send(string eventName)
        {
            Send(eventName, new Dictionary<string, object>());
        }

        /// <summary>
        /// Sends an event to the server.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Send(string eventName, Dictionary<string, object> args)
        {
            ZPackage pkg = new ZPackage();
            pkg.Write(eventName); // Write event name
            Shared.Networking.SerializeDictionary(ref pkg, args);
            ZRoutedRpc.instance.InvokeRoutedRPC(GetServerPeerID(), "LuaxeNetworkEvent", new object[] { pkg });
        }
    }
}
