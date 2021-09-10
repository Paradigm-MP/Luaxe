using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Server
{
	[BepInPlugin(Constants.ModInfo.modGUID, Constants.ModInfo.modName, Constants.ModInfo.modVersion)]
	public class Core : BaseUnityPlugin
	{
		private readonly Harmony harmony = new Harmony(Constants.ModInfo.modGUID);

		void Awake()
		{
			InitializeAll();
			harmony.PatchAll();

			Shared.Logging.log.LogMessage("Successfully Initialized!");
		}

		void InitializeAll()
		{
			Shared.Logging.Initialize();
			Shared.Logging.log.LogMessage("Initializing server...");

			Networking.Initialize();
			Console.Input.Initialize();
			Console.Commands.Initialize();
		}

	}
}
