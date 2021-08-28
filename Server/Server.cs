using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Server
{
	[BepInPlugin(modGUID, modName, modVersion)]
	public class Luaxe : BaseUnityPlugin
	{
		private const string modGUID = "Paradigm.Luaxe.Server";
		private const string modName = "Luaxe Server";
		private const string modVersion = "0.0.1";

		private readonly Harmony harmony = new Harmony(modGUID);
		void Awake()
		{
			harmony.PatchAll();
		}

		/*[HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
		class FixOnSwiming
		{
			public static void Prefix(Player __instance)
			{
				Player player = __instance;
				player.m_jumpForce = 100f;
			}
		}*/
	}
}
