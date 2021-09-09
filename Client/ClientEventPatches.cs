using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.UI;

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

    [HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
    class PlayerJump
    {
        public static void Prefix(Player __instance)
        {
            Shared.Events.EventSystem.Broadcast(new Luaxe.Client.Events.PlayerJumpGameEvent(__instance));
        }
    }


    [HarmonyPatch(typeof(Character), nameof(Character.Damage))]
    class CharacterDamage
    {
        public static bool Prefix(HitData hit, Character __instance)
        {
            return Shared.Events.EventSystem.Broadcast(new Luaxe.Client.Events.CharacterDamagedGameEvent(hit, __instance));
        }
    }

    [HarmonyPatch(typeof(Chat), "InputText")]
    public static class ChatHandler
    {
        private static bool Prefix(ref Chat __instance)
        {
            return Shared.Events.EventSystem.Broadcast(new Luaxe.Client.Events.ChatMessage(__instance.m_input.text));
        }
    }
}
