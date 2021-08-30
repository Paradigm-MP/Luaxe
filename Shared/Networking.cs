using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;

namespace Luaxe.Shared
{
    public class Networking
    {

        /// <summary>
        /// Returns whether 
        /// </summary>
        /// <returns></returns>
        public static bool IsServer()
        {
            return ZNet.instance.IsServer() || ZNet.instance.IsDedicated() || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }
    }
}
