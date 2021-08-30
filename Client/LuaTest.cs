using BepInEx;
using HarmonyLib;

namespace Client
{
	/*[BepInPlugin(modGUID, modName, modVersion)]
	[BepInProcess("valheim.exe")]
	public class LuaTest : BaseUnityPlugin
	{
		private const string modGUID = "Paradigm.Luaxe.Client";
		private const string modName = "Luaxe Client";
		private const string modVersion = "0.0.1";

		private readonly Harmony harmony = new Harmony(modGUID);
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
	}*/
}
