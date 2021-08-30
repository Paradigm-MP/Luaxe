using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server
{
	[BepInPlugin(Constants.ModInfoConstants.modGUID, Constants.ModInfoConstants.modName, Constants.ModInfoConstants.modVersion)]
	public class Core : BaseUnityPlugin
	{
		private readonly Harmony harmony = new Harmony(Constants.ModInfoConstants.modGUID);
		void Awake()
		{
			harmony.PatchAll();
			Networking.Initialize();
		}
	}
}
