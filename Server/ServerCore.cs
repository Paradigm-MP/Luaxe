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

			Shared.UnityObserver.Awake?.Invoke();
		}

		void InitializeAll()
		{
			Networking.Initialize();
		}
		void Start()
		{
			Shared.UnityObserver.Start?.Invoke();
		}

	}
}
