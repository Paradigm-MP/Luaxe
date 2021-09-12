using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
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
            Shared.Logging.log.LogInfo("Networking intialized.");
        }

        static void Awake()
        {
            Shared.Events.EventSystem.AddListener<Events.NewConnectionGameEvent>(OnNewConnectionGameEvent);
            Shared.Events.EventSystem.AddListener<Events.ConsoleCommand>(OnConsoleCommand);
            Shared.Logging.log.LogInfo("Networking awake.");
        }

        static void Start()
        {
            Shared.Logging.log.LogInfo("Networking start.");
            Shared.Logging.log.LogInfo($"Registering RPC callback for LuaxeNetworkEvent");
            ZRoutedRpc.instance.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
        }

        static bool OnConsoleCommand(Events.ConsoleCommand evt)
        {
            if (!evt.isInternal) { return true; }

            if (evt.command == "testnet")
            {
                Shared.Logging.log.LogInfo($"Broadcasting test network event to all clients.");
                Networking.Broadcast("testevent", new Dictionary<string, object>()
                {
                    {"testkey1", "testvalue1" },
                    {"testkey2", 55.3 },
                    {"testkey3", new Vector3(4, 2.1f, -33.22f) }
                });
                Shared.Logging.log.LogInfo($"Broadcasted test network event to all clients.");
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

        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got Server RPC_LuaxeNetworkEvent");
                Shared.Networking.NetworkEventData ned = Shared.Networking.DeserializePackageToNetworkEventData(package);
                Shared.Events.EventSystem.Broadcast(new Events.NetworkEvent(ned));
                ned.LogMetadata();
                ned.LogArgs();

                Shared.Logging.log.LogInfo($"Sending response...");
                Send(peer, "testresponsefromserver");
            }
        }
        static void Send(ZNetPeer peer, string eventName)
        {
            Send(peer, eventName, new Dictionary<string, object>());
        }

        /// <summary>
        /// Sends an event to a single player through their ZNetPeer instance.
        /// </summary>
        /// <param name="peer"></param>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Send(ZNetPeer peer, string eventName, Dictionary<string, object> args)
        {
            ZPackage pkg = new ZPackage();
            pkg.Write(eventName); // Write event name
            Shared.Networking.SerializeDictionary(ref pkg, args);
            ZRoutedRpc.instance.InvokeRoutedRPC(peer.m_uid, "LuaxeNetworkEvent", new object[] { pkg });
        }

        static void Broadcast(string eventName)
        {
            Broadcast(eventName, new Dictionary<string, object>());
        }

        /// <summary>
        /// Broadcasts an event to all players through RPC.
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        static void Broadcast(string eventName, Dictionary<string, object> args)
        {
            ZPackage pkg = new ZPackage();
            pkg.Write(eventName); // Write event name
            Shared.Networking.SerializeDictionary(ref pkg, args);
            ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "LuaxeNetworkEvent", new object[] { pkg });
        }
    }
}
