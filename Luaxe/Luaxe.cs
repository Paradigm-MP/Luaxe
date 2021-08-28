using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe
{
    [BepInPlugin("Paradigm.Luaxe", "Luaxe", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class Luaxe : BaseUnityPlugin
    {
		private readonly Harmony harmony = new Harmony("Paradigm.Luaxe");
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
    }
}
