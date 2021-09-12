using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

namespace Luaxe.Shared
{
    public class Networking
    {
        /// <summary>
        /// Container class to hold metadata and args after deserializing a network event.
        /// </summary>
        public class NetworkEventData
        {
            public Dictionary<string, object> metaData;
            public Dictionary<string, object> args;

            public NetworkEventData()
            {
                metaData = new Dictionary<string, object>();
                args = new Dictionary<string, object>();
            }

            public NetworkEventData(Dictionary<string, object> metaData, Dictionary<string, object> args)
            {
                this.metaData = metaData;
                this.args = args;
            }

            public void LogMetadata()
            {
                Shared.Logging.log.LogMessage($"metaData: ----------");
                foreach (var pair in this.metaData)
                {
                    Shared.Logging.log.LogMessage($"metaData: [{pair.Key}] = {pair.Value}");
                }
            }

            public void LogArgs()
            {
                Shared.Logging.log.LogMessage($"args: ----------");
                foreach (var pair in this.args)
                {
                    Shared.Logging.log.LogMessage($"args: [{pair.Key}] = {pair.Value}");
                }
            }
        }

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

        /// <summary>
        /// Serializes and writes a generic object if it is a supported type.
        /// Add more types here.
        /// </summary>
        /// <param name="pkg"></param>
        /// <param name="obj"></param>
        public static void SerializeAndWriteObject(ref ZPackage pkg, object obj)
        {
            // TODO: add support for generic lua types and tables
            if (obj is int @int)
            {
                pkg.Write("int");
                pkg.Write(@int);
            }
            else if (obj is uint @uint)
            {
                pkg.Write("uint");
                pkg.Write(@uint);
            }
            else if (obj is long @long)
            {
                pkg.Write("long");
                pkg.Write(@long);
            }
            else if (obj is float @single)
            {
                pkg.Write("single");
                pkg.Write(@single);
            }
            else if (obj is double @double)
            {
                pkg.Write("double");
                pkg.Write(@double);
            }
            else if (obj is bool @boolean)
            {
                pkg.Write("bool");
                pkg.Write(@boolean);
            }
            else if (obj is string @string)
            {
                pkg.Write("string");
                pkg.Write(@string);
            }
            else if (obj is ZPackage @package)
            {
                pkg.Write("ZPackage");
                pkg.Write(@package);
            }
            else if (obj is Vector3)
            {
                pkg.Write("Vector3");
                pkg.Write(((Vector3)obj).x);
                pkg.Write(((Vector3)obj).y);
                pkg.Write(((Vector3)obj).z);
            }
            else if (obj is Quaternion)
            {
                pkg.Write("Quaternion");
                pkg.Write(((Quaternion)obj).x);
                pkg.Write(((Quaternion)obj).y);
                pkg.Write(((Quaternion)obj).z);
                pkg.Write(((Quaternion)obj).w);
            }
            else if (obj is ZDOID)
            {
                pkg.Write("ZDOID");
                pkg.Write((ZDOID)obj);
            }
            else if (obj is HitData)
            {
                pkg.Write("HitData");
                (obj as HitData).Serialize(ref pkg);
            }
            else
            {
                Shared.Logging.log.LogError($"Failed to serialize object: ({obj.GetType()}) {obj.ToString()}");
            }
        }

        /// <summary>
        /// Deserialize a package that was serialized by SerializeDictionary
        /// </summary>
        /// <param name="pkg"></param>
        public static NetworkEventData DeserializePackageToNetworkEventData(ZPackage pkg)
        {
            NetworkEventData ned = new NetworkEventData();

            // TODO: add support for generic lua types and tables
            ned.metaData.Add("eventName", pkg.ReadString());

            int numArgs = pkg.ReadInt();
            ned.metaData.Add("numArgs", numArgs);

            for (int i = 1; i <= numArgs; i++)
            {
                string key = pkg.ReadString();
                string argType = pkg.ReadString();

                if (argType == "int")
                {
                    ned.args.Add(key, pkg.ReadInt());
                }
                else if (argType == "uint")
                {
                    ned.args.Add(key, pkg.ReadUInt());
                }
                else if (argType == "long")
                {
                    ned.args.Add(key, pkg.ReadLong());
                }
                else if (argType == "single") // Float
                {
                    ned.args.Add(key, pkg.ReadSingle());
                }
                else if (argType == "double")
                {
                    ned.args.Add(key, pkg.ReadDouble());
                }
                else if (argType == "bool")
                {
                    ned.args.Add(key, pkg.ReadBool());
                }
                else if (argType == "string")
                {
                    ned.args.Add(key, pkg.ReadString());
                }
                else if (argType == "ZPackage")
                {
                    ned.args.Add(key, pkg.ReadPackage());
                }
                else if (argType == "Vector3")
                {
                    Vector3 vector = new Vector3(pkg.ReadSingle(), pkg.ReadSingle(), pkg.ReadSingle());
                    ned.args.Add(key, vector);
                }
                else if (argType == "Quaternion")
                {
                    Quaternion quaternion = new Quaternion(pkg.ReadSingle(), pkg.ReadSingle(), pkg.ReadSingle(), pkg.ReadSingle());
                    ned.args.Add(key, quaternion);
                }
                else if (argType == "ZDOID")
                {
                    ned.args.Add(key, pkg.ReadZDOID());
                }
                else if (argType == "HitData")
                {
                    HitData hitData = new HitData();
                    hitData.Deserialize(ref pkg);
                    ned.args.Add(key, hitData);
                }
            }

            if (ned.args.Count == 0 && ned.metaData.Count == 0)
            {
                Shared.Logging.log.LogError($"Failed to deserialize package. No metaData or args found.");
            }

            return ned;
        }

        public static void SerializeDictionary(ref ZPackage pkg, Dictionary<string, object> args)
        {
            // First write count of args
            pkg.Write(args.Count);
            foreach (var item in args)
            {
                // Write key, type, then data
                pkg.Write(item.Key);
                SerializeAndWriteObject(ref pkg, item.Value);
            }
        }
    }
}
