using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Luaxe
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInProcess("valheim.exe")]
    public class Luaxe : BaseUnityPlugin
    {
        private const string modGUID = "Paradigm.Luaxe";
        private const string modName = "Luaxe by Paradigm";
        private const string modVersion = "0.0.1";
        private readonly Harmony harmony = new Harmony(modGUID);
        void Awake()
        {
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Character), "Start")]
        class TestCharacter_Patch
        {
            [HarmonyPrefix]
            static void setCharacterJumpPatch(ref float ___m_jumpForce)
            {
                ___m_jumpForce = 100f;
            }
        }

    }
}
