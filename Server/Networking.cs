using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;

namespace Luaxe.Server
{
    public static class Networking
    {
        public static void Initialize()
        {
            Shared.UnityObserver.Awake += Awake;
            Shared.UnityObserver.Start += Start;
            Shared.Logging.log.LogMessage("Networking intialized.");
        }

        static void Awake()
        {
            Shared.Events.EventSystem.AddListener<Events.NewConnectionGameEvent>(OnNewConnectionGameEvent);
            Shared.Events.EventSystem.AddListener<Events.ConsoleCommand>(OnConsoleCommand);
            Shared.Logging.log.LogMessage("Networking awake.");
        }

        static void Start()
        {
            Shared.Logging.log.LogMessage("Networking start.");
            Shared.Logging.log.LogMessage($"Registering RPC callback for LuaxeNetworkEvent");
            ZRoutedRpc.instance.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
        }

        static bool OnConsoleCommand(Events.ConsoleCommand evt)
        {
            if (evt.isInternal) { return true; }

            if (evt.command == "testnet")
            {
                Broadcast("testevent", new object[]
                {
                    53,
                    "hello"
                });
                Shared.Logging.log.LogMessage($"Broadcasted test network event to all clients.");
                return false;
            }

            return true;
        }

        static bool OnNewConnectionGameEvent(Events.NewConnectionGameEvent evt)
        {
            string peerSteamID = ((ZSteamSocket)evt.peer.m_socket).GetPeerID().m_SteamID.ToString();
            Shared.Logging.log.LogInfo($"New connection: {evt.peer.m_playerName} SteamID: {peerSteamID}");
            return true;
        }

        static void Subscribe(string eventName)
        {
        }

        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got Server RPC_LuaxeNetworkEvent");
                Send(peer, "testnet2");
            }
        }

        /// <summary>
        /// Sends an event to a single player through their ZNetPeer instance.
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Send(ZNetPeer peer, string eventName, params object[] args)
        {
            peer.m_rpc.Invoke("LuaxeNetworkEvent", args);
        }

        /// <summary>
        /// Broadcasts an event to all players through RPC.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Broadcast(string eventName, params object[] args)
        {
            ZRoutedRpc.instance.InvokeRoutedRPC("LuaxeNetworkEvent", args);
        }
    }
}
