using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server
{
	[BepInPlugin(modGUID, modName, modVersion)]
	public class Core : BaseUnityPlugin
	{
		private const string modGUID = "Paradigm.Luaxe.Server";
		private const string modName = "Luaxe Server";
		private const string modVersion = "0.0.1";

		private readonly Harmony harmony = new Harmony(modGUID);
		void Awake()
		{
			harmony.PatchAll();
		}
	}
}
