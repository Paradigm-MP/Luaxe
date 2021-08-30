using Client.LuaFunctions;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class LuaStateBuilder
    {
        static public Lua BuildState()
        {
            Lua luaState = new Lua();

            // Changes encoding from ASCII (default) to UTF8
            luaState.State.Encoding = Encoding.UTF8;

            AddLuaFunction(luaState, typeof(LuaPrint), "Print", "print");

            // To access any.NET assembly to create objects, events etc inside Lua you need to ask NLua to use CLR as a Lua package
            luaState.LoadCLRPackage();

            luaState.DoString(@" import ('assembly_valheim') ");

            return luaState;
        }

        static protected void AddLuaFunction(Lua luaState, Type classType, string methodName, string luaFunctionName)
        {
            MethodInfo methodInfo = classType.GetMethod(methodName);
            luaState.RegisterFunction(luaFunctionName, methodInfo);
        }
    }
}
