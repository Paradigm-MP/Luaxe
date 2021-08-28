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
```
5. Navigate to your Valheim game directory. Copy the following files from the `unstripped_corlib` folder to the `libs` folder:
```
UnityEngine.CoreModule.dll
UnityEngine.dll
```
6. Open up Luaxe.sln and you're all set!