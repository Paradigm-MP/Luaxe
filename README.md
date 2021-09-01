# Luaxe

Create wonderous gamemodes for Valheim with Lua. [Get started here.](https://luaxe.dev/)

## 🛑 Disclaimer 🛑
This is a **highly WIP** project. Not intended for use..._yet_.

## Setting Up for Development

These steps are only needed if you plan on developing the Luaxe mod. If you are writing Lua scripts to be used with the mod, you don't need to complete these steps, but instead read the [documentation](https://luaxe.dev/) to make sure you install Luaxe properly.


1. Download [BepInEx for Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/). Follow the instructions to install BepInEx to Valheim. Also make sure to install it to your server.
2. Create a `Libs` folder (right next to this file).
3. Copy the following files from BepInEx to the `Libs` folder:
```
0Harmony.dll
BepInEx.dll
```
4. Navigate to your Valheim game directory. Copy the following files from the `valheim_Data\Managed` folder to the `Libs` folder:
```
assembly_valheim.dll
assembly_utils.dll
```
5. Navigate to your Valheim game directory. Copy the following files from the `unstripped_corlib` folder to the `Libs` folder:
```
UnityEngine.InputLegacyModule.dll
UnityEngine.InputModule.dll
UnityEngine.PhysicsModule.dll
UnityEngine.CoreModule.dll
UnityEngine.dll
```
6. Create an environment variable called `VALHEIM_BEPINEX_PLUGINS_CLIENT` and set it to the path of your BepInEx plugins folder in your game, **including the end \\**. Example:
```
VALHEIM_BEPINEX_PLUGINS=B:\Programs\Steam\Games\steamapps\common\Valheim\BepInEx\plugins\
```
7. Create an environment variable called `VALHEIM_BEPINEX_PLUGINS_SERVER` and set it to the path of your BepInEx plugins folder in your server, **including the end \\**. Example:
```
VALHEIM_BEPINEX_PLUGINS=C:\Program Files (x86)\Steam\steamapps\common\Valheim dedicated server\BepInEx\plugins\
```
8. Open up Luaxe.sln and you're all set! If you opened it before creating your environment variables, you will need to restart it.
