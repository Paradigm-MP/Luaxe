﻿using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Client
{
	/*[BepInPlugin(modGUID, modName, modVersion)]
	[BepInProcess("valheim.exe")]
	public class Luaxe : BaseUnityPlugin
	{
		private const string modGUID = "Paradigm.Luaxe.Client";
		private const string modName = "Luaxe Client";
		private const string modVersion = "0.0.1";

		private readonly Harmony harmony = new Harmony(modGUID);
		void Awake()
		{
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
		class FixOnSwiming
		{
			public static void Prefix(Player __instance)
			{
				Player player = __instance;
				player.m_jumpForce = 100f;
			}
		}
	}*/
}
