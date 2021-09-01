﻿using BepInEx;
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

			Shared.Logging.log.LogInfo("Initialized!");
		}

		void InitializeAll()
		{
			Networking.Initialize();
			Console.Input.Initialize();
			Console.Commands.Initialize();
			Shared.Logging.Initialize();
		}

		void Start()
		{
			Shared.UnityObserver.Start?.Invoke();
		}

	}
}
