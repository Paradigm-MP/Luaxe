﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Luaxe.Server.Patches.Events
{
    [HarmonyPatch(typeof(ZNet), nameof(ZNet.RPC_PeerInfo))]
    class OnRPC_PeerInfo
    {
        public static void Postfix(ZRpc rpc, ZPackage pkg, ZNet __instance)
        {
            ZNetPeer peer = Traverse.Create(__instance).Method("GetPeer", rpc).GetValue<ZNetPeer>();
            Shared.Events.EventSystem.Broadcast(new Luaxe.Server.Events.NewConnectionGameEvent(rpc, pkg, peer, __instance));
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
