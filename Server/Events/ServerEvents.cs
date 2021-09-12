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

    public class ConsoleCommand : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.ConsoleCommand;
        public string command;
        public bool isInternal = false;

        public ConsoleCommand(string command)
        {
            this.command = command;
        }

        public ConsoleCommand(string command, bool isInternal)
        {
            this.command = command;
            this.isInternal = isInternal;
        }
    }

    public class ServerStopCommand : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.ServerStopCommand;

        public ServerStopCommand() { }
    }

    public class NetworkEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.NetworkEvent;
        public Shared.Networking.NetworkEventData ned;

        public NetworkEvent(Shared.Networking.NetworkEventData ned)
        {
            this.ned = ned;
        }
    }
}
