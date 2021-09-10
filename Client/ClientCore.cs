using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace Luaxe.Client
{
	[BepInPlugin(Constants.ModInfo.modGUID, Constants.ModInfo.modName, Constants.ModInfo.modVersion)]
	[BepInProcess("valheim.exe")]
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
			Shared.Logging.log.LogMessage("Initializing client...");

			Networking.Initialize();

			Shared.Events.EventSystem.AddListener<Events.PlayerDeathGameEvent>(OnPlayerDeathEvent);
			Shared.Events.EventSystem.AddListener<Events.PlayerJumpGameEvent>(OnPlayerJumpEvent);
			Shared.Events.EventSystem.AddListener<Events.CharacterDamagedGameEvent>(OnCharacterDamagedEvent);
			Shared.Events.EventSystem.AddListener<Events.LocalPlayerChat>(OnLocalPlayerChat);
		}

		bool OnLocalPlayerChat(Events.LocalPlayerChat evt)
		{
			Shared.Logging.log.LogInfo($"LocalPlayerChat: {evt.text}");
			return true;
		}

		bool OnPlayerJumpEvent(Events.PlayerJumpGameEvent evt)
		{
			Shared.Logging.log.LogInfo($"Player jump event!");
			return false;
        }

		bool OnPlayerDeathEvent(Events.PlayerDeathGameEvent evt)
		{
			Shared.Logging.log.LogInfo($"Player death event! Player died: {evt.player.name} Location: {evt.player.gameObject.transform.position}");
			return true;
		}

		bool OnCharacterDamagedEvent(Events.CharacterDamagedGameEvent evt)
		{
			Shared.Logging.log.LogInfo($"Character damaged! Char: {evt.character.m_name} damage: {evt.hit.m_damage.m_damage}");
			// Return false to block all damage
			return false;
		}
	}
}
