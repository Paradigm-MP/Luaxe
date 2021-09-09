using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luaxe.Client
{
    public static class Networking
    {
        public static void Initialize()
        {
            if (Shared.Networking.IsServer())
            {
                Shared.Logging.log.LogError("Failed to initialize Client networking core. You are running the client mod on the server.");
                return;
            }

            Shared.UnityObserver.Awake += Awake;
        }
        static void Awake()
        {

        }

        static void Subscribe(string eventName)
        {
            ZNet.instance.m_routedRpc.Register<ZPackage>("LuaxeNetworkEvent", RPC_LuaxeNetworkEvent);
        }

        private static void RPC_LuaxeNetworkEvent(long sender, ZPackage package)
        {
            ZNetPeer peer = ZNet.instance.GetPeer(sender);
            if (peer != null)
            {
                Shared.Logging.log.LogInfo($"Got RPC_LuaxeNetworkEvent");
            }
        }


    }
}
