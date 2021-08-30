using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server.Events
{
    public class NewConnectionGameEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.NewConnection;
        public ZNetPeer peer;
        public ZNet znet;

        public NewConnectionGameEvent(ZNetPeer peer, ZNet znet)
        {
            this.peer = peer;
            this.znet = znet;
        }
    }
}
