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

			Shared.UnityObserver.Awake?.Invoke();

			Shared.Logging.log.LogInfo("Initialized!");
		}

		void InitializeAll()
		{
			Shared.Events.EventSystem.AddListener<Events.PlayerDeathGameEvent>(OnPlayerDeathEvent);
			Shared.Events.EventSystem.AddListener<Events.PlayerJumpGameEvent>(OnPlayerJumpEvent);
			Shared.Events.EventSystem.AddListener<Events.CharacterDamagedGameEvent>(OnCharacterDamagedEvent);

			Shared.Logging.Initialize();
		}

		void Start()
		{
			Shared.UnityObserver.Start?.Invoke();
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
