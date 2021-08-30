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
            Debug.Log($"New connection: {evt.peer.m_playerName} UID: {evt.peer.m_uid}");
            return true;
        }
    }
}
