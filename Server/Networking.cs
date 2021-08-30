using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering;

namespace Luaxe.Server
{
    class Networking
    {
        void Start()
        {
            if (!Shared.Networking.IsServer())
            {
                throw new System.Exception("Failed to initialize Server networking core. You are running the server mod on the client.");
            }
        }

        void Send()
        {

        }
    }
}
