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
            if (!Shared.Networking.IsServer())
            {
                Shared.Logging.log.LogError("Failed to initialize Server networking core. You are running the server mod on the client.");
                return;
            }

            Shared.UnityObserver.Awake += Awake;
        }

        static void Awake()
        {
            Shared.Events.EventSystem.AddListener<Events.NewConnectionGameEvent>(OnNewConnectionGameEvent);
        }

        static bool OnNewConnectionGameEvent(Events.NewConnectionGameEvent evt)
        {
            string peerSteamID = ((ZSteamSocket)evt.peer.m_socket).GetPeerID().m_SteamID.ToString();
            Shared.Logging.log.LogInfo($"New connection: {evt.peer.m_playerName} SteamID: {peerSteamID}");
            return true;
        }

        static void Subscribe(string eventName)
        {
            ZRoutedRpc.instance.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
        }

        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got RPC_LuaxeNetworkEvent");
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
            peer.m_rpc.Invoke(eventName, args);
        }

        /// <summary>
        /// Broadcasts an event to all players through RPC.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Broadcast(string eventName, params object[] args)
        {
            ZRoutedRpc.instance.InvokeRoutedRPC(eventName, args);
        }
    }
}
