# Luaxe

## Setting Up for Development

1. Download [BepInEx for Valheim](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/). Follow the instructions to install BepInEx to Valheim.
2. Create a `libs` folder in the root of Luaxe (right where this readme is).
3. Copy the following files from BepInEx to the `libs` folder:
```
0Harmony.dll
BepInEx.dll
```
4. Navigate to your Valheim game directory. Copy the following files from the `valheim_Data\Managed` folder to the `libs` folder:
```
assembly_valheim.dll
assembly_utils.dll
```
5. Navigate to your Valheim game directory. Copy the following files from the `unstripped_corlib` folder to the `libs` folder:
```
UnityEngine.InputLegacyModule.dll
UnityEngine.InputModule.dll
UnityEngine.PhysicsModule.dll
UnityEngine.CoreModule.dll
UnityEngine.dll
```
6. Create an environment variable called `VALHEIM_BEPINEX_PLUGINS` and set it to the path of your BepInEx plugins folder, **including the end \\**. Example:
```
VALHEIM_BEPINEX_PLUGINS=B:\Programs\Steam\Games\steamapps\common\Valheim\BepInEx\plugins\
```
7. Open up Luaxe.sln and you're all set!