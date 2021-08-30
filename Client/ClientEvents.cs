using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Client.Events
{
    public class PlayerDeathGameEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.PlayerDeath;
        public Player player;

        public PlayerDeathGameEvent(Player player) => this.player = player;
    }

}
