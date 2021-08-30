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
            bool isServer = (ZNet.instance != null) ?
                ZNet.instance.IsServer() || ZNet.instance.IsDedicated() :
                false;

            return isServer || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
        }
    }
}
