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

    [HarmonyPatch(typeof(Chat), nameof(Chat.InputText))]
    public static class ChatHandler
    {
        private static bool Prefix(ref Chat __instance)
        {
            return Shared.Events.EventSystem.Broadcast(new Luaxe.Client.Events.LocalPlayerChat(__instance.m_input.text));
        }
    }

    /// <summary>
    /// Patches the normal chat RPC to first RPC to server so the server can validate then RPC to all clients.
    /// </summary>
    [HarmonyPatch(typeof(Chat), nameof(Chat.SendText))]
    public static class ChatHandlerToServer
    {
        private static bool Prefix(Talker.Type type, string text, ref Chat __instance)
        {
            Player localPlayer = Player.m_localPlayer;
            if (localPlayer)
            {
                if (type == Talker.Type.Shout)
                {
                    ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.Everybody, "ChatMessage", new object[]
                    {
                        localPlayer.GetHeadPoint(),
                        2,
                        localPlayer.GetPlayerName(),
                        text
                    });
                    return false;
                }
                localPlayer.GetComponent<Talker>().Say(type, text);
            }
            return false;
        }
    }

    /// <summary>
    /// Patches the normal chat RPC to first RPC to server so the server can validate then RPC to all clients.
    /// </summary>
    [HarmonyPatch(typeof(Talker), nameof(Talker.Say))]
    public static class SayHandlerToServer
    {
        private static bool Prefix(Talker.Type type, string text, ref Talker __instance)
        {
            ZLog.Log(string.Concat(new object[]
            {
                "Saying ",
                type,
                "  ",
                text
            }));
            ZRoutedRpc.instance.InvokeRoutedRPC(ZNetView.Everybody, "Say", new object[]
            {
                (int)type,
                Game.instance.GetPlayerProfile().GetName(),
                text
            });

            return false;
        }
    }

    [HarmonyPatch(typeof(Game), "Start")]
    public static class GameStartPatch
    {
        private static void Prefix(ref Game __instance)
        {
            Shared.UnityObserver.Start?.Invoke();
        }
    }

    [HarmonyPatch(typeof(Game), "Awake")]
    public static class GameAwakePatch
    {
        private static void Prefix(ref Game __instance)
        {
            Shared.UnityObserver.Awake?.Invoke();
        }
    }
}
