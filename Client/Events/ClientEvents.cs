﻿using BepInEx;
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

    public class PlayerJumpGameEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.PlayerJump;
        public Player player;

        public PlayerJumpGameEvent(Player player) => this.player = player;
    }

    public class CharacterDamagedGameEvent : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.CharacterDamaged;
        public Character character;
        public HitData hit;

        public CharacterDamagedGameEvent(HitData hit, Character character)
        {
            this.character = character;
            this.hit = hit;
        }
    }

    public class LocalPlayerChat : Luaxe.Shared.Events.GameEvent
    {
        public override string luaEventName => Constants.Events.LocalPlayerChat;
        public string text;

        public LocalPlayerChat(string text)
        {
            this.text = text;
        }
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
