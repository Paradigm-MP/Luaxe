using BepInEx;
using HarmonyLib;

namespace Luaxe
{
	[BepInPlugin("Paradigm.Luaxe", "Luaxe", "1.0.0")]
	[BepInProcess("valheim.exe")]
	public class LuaTest : BaseUnityPlugin
	{
		private readonly Harmony harmony = new Harmony("Paradigm.Luaxe");
		void Awake()
		{
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(Player), nameof(Player.OnJump))]
		class RunLuaOnJump
		{
			public static void Prefix(Player __instance)
			{
				var luaState = LuaStateBuilder.BuildState();
				luaState.DoString(@"print('Lua print() meet Bepinex console!')");

				Player player = __instance;
				player.m_jumpForce = 100f;
			}
		}
	}
}
