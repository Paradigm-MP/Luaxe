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
                throw new System.Exception("Failed to initialize Server networking core. You are running the server mod on the client.");
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
    }
}
