using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server.Events
{
    public class NewConnectionGameEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.NewConnection;
        public ZRpc rpc;
        public ZNetPeer peer;
        public ZPackage pkg;
        public ZNet znet;

        public NewConnectionGameEvent(ZRpc rpc, ZPackage pkg, ZNetPeer peer, ZNet znet)
        {
            this.rpc = rpc;
            this.pkg = pkg;
            this.peer = peer;
            this.znet = znet;
        }
    }
}
