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
			// Or Shared.Events.EventSystem.AddListener<Luaxe.Client.Events.PlayerDeathGameEvent>(PlayerDeathEvent);
			// Assuming you have a method in this class called PlayerDeathEvent
			Shared.Events.EventSystem.AddListener<Events.PlayerDeathGameEvent>(OnPlayerDeathEvent);
			Shared.Events.EventSystem.AddListener<Events.PlayerJumpGameEvent>(OnPlayerJumpEvent);
			Shared.Events.EventSystem.AddListener<Events.CharacterDamagedGameEvent>(OnCharacterDamagedEvent);

			harmony.PatchAll();
		}

		bool OnPlayerJumpEvent(Events.PlayerJumpGameEvent evt)
		{
			Debug.Log($"Player jump event!");
			return false;
        }

		bool OnPlayerDeathEvent(Events.PlayerDeathGameEvent evt)
		{
			Debug.Log($"Player death event! Player died: {evt.player.name} Location: {evt.player.gameObject.transform.position}");
			return true;
		}

		bool OnCharacterDamagedEvent(Events.CharacterDamagedGameEvent evt)
		{
			Debug.Log($"Character damaged! Char: {evt.character.m_name} damage: {evt.hit.m_damage.m_damage}");
			// Return false to block all damage
			return false;
		}
	}
}
