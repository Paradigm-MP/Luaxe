using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server.Patches.Events
{
    [HarmonyPatch(typeof(ZNet), nameof(ZNet.OnNewConnection))]
    class OnNewConnection
    {
        public static void Prefix(ZNetPeer peer, ZNet __instance)
        {
            Shared.Events.EventSystem.Broadcast(new Luaxe.Server.Events.NewConnectionGameEvent(peer, __instance));
        }
    }
}
