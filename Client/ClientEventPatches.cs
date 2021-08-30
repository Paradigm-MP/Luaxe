using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Client.Patches.Events
{
    [HarmonyPatch(typeof(Player), nameof(Player.OnDeath))]
    class PlayerDeath
    {
        public static void Prefix(Player __instance)
        {
            Shared.Events.EventSystem.Broadcast(new Luaxe.Client.Events.PlayerDeathGameEvent(__instance));
        }
    }
}
